using EmployeeBirthdayGiftVotingSystem.Models.Vote;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBirthdayGiftVotingSystem.Views.Shared.Components.VoteCard
{
    public class VoteCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(VoteIndexViewModel vote)
        {
            return vote.IsActive ? View("ActiveVoteCard", vote) : View("InactiveVoteCard", vote);
        }
    }
}
