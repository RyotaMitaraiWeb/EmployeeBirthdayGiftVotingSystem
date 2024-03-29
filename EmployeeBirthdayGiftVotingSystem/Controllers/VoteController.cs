﻿using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Models.Gift;
using EmployeeBirthdayGiftVotingSystem.Models.Vote;
using EmployeeBirthdayGiftVotingSystem.Services.VoteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeBirthdayGiftVotingSystem.Controllers
{
    [Authorize]
    public class VoteController(IVoteService voteService, IGiftService giftService) : Controller
    {
        private readonly IVoteService _voteService = voteService;
        private readonly IGiftService _giftService = giftService;

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var users = await this._voteService.GetVotesIndexList(id);
            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> EndVote(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? votingId = await this._voteService.EndVote(userId, id);
            if (votingId == null)
            {
                return Redirect(nameof(this.Index));
            }

            return Redirect($"/Vote/Details/{id}");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            string creatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            DateTime today = DateTime.UtcNow;

            IEnumerable<CreateVoteViewModel> users = await this._voteService.GetAllUsersThatCanHaveAVote(creatorId, today);
            var model = new CreateVoteListViewModel()
            {
                EmployeeId = string.Empty,
                AvailableUsers = users,
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateVoteListViewModel vote)
        {
            string creatorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            DateTime today = DateTime.UtcNow;

            CreateVoteViewModel createVote = new() { EmployeeId = vote.EmployeeId };

            var result = await this._voteService.CreateVote(createVote, creatorId, today);
            if (result == null)
            {
                IEnumerable<CreateVoteViewModel> users = await this._voteService.GetAllUsersThatCanHaveAVote(creatorId, today);
                var model = new CreateVoteListViewModel()
                {
                    EmployeeId = string.Empty,
                    AvailableUsers = users,
                };

                return View(model);
            }

            return Redirect($"/Vote/Details/{result}");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var vote = await this._voteService.GetVoteDetails(id);
            if (vote == null)
            {
                return NotFound();
            }

            var userId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (Guid.Equals(userId, vote.EmployeeId))
            {
                return NotFound();
            }

            int? giftVoteId = vote.UserGiftVotes.FirstOrDefault(ugv => Guid.Equals(userId, ugv.VoterId))?.GiftId;

            var gifts = await this._giftService.GetGiftsForVoting();
            var model = new VoteDetailsViewModel()
            {
                VoteId = id,
                GiftVoteId = giftVoteId,
                Employee = vote.Employee,
            };

            if (vote.IsActive)
            {
                model.Gifts = gifts;
            }
            else
            {
                ArrangeResults(model, gifts, vote.UserGiftVotes);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CastVote(int id, VoteDetailsViewModel vote)
        {
            int giftId = vote.GiftVoteId ?? 0;
            if (giftId == 0)
            {
                return Redirect($"/Vote/Details/{id}");
            }

            bool exists = await this._giftService.CheckIfGiftExists(giftId);
            if (!exists)
            {
                return Redirect($"/Vote/Details/{id}");
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            int? voteId = await this._voteService.CastVote(userId, id, giftId);
            if (voteId == null)
            {
                return Redirect($"/Vote/Details/{id}");
            }

            return Redirect($"/Vote/Details/{id}");
        }

        private static void ArrangeResults(VoteDetailsViewModel model, IEnumerable<GiftVoteViewModel> gifts, IEnumerable<UserGiftVote> userGiftVotes)
        {
            Dictionary<string, IEnumerable<string>> votes = [];
            foreach (var gift in gifts)
            {
                votes[gift.Name] = new List<string>();
            }

            votes["Has not voted"] = new List<string>();

            foreach (var userVote in userGiftVotes)
            {
                string? gift = userVote.Gift?.Name;
                if (userVote.GiftId == null || gift == null)
                {
                    votes["Has not voted"] = votes["Has not voted"].Append(userVote.Voter.UserName!);
                }
                else
                {
                    votes[gift] = votes[gift].Append(userVote.Voter.UserName!);
                }
            }

            model.UserGiftVotes = votes;
        }
    }
}
