using EmployeeBirthdayGiftVotingSystem.Models.Vote;
using EmployeeBirthdayGiftVotingSystem.Services.VoteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeBirthdayGiftVotingSystem.Controllers
{
    [Authorize]
    public class VoteController(IVoteService voteService) : Controller
    {
        private readonly IVoteService _voteService = voteService;

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
    }
}
