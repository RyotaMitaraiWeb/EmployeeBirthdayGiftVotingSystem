using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data.Entities;
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

            return Redirect("/Home/Index");
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
                Gifts = gifts,
                Employee = vote.Employee,
            };

            return View(model);
        }
    }
}
