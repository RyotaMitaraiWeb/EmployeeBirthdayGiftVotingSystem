using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Models.Gift;

namespace EmployeeBirthdayGiftVotingSystem.Models.Vote
{
    public class VoteDetailsViewModel
    {
        public int VoteId { get; set; }
        public IEnumerable<GiftVoteViewModel> Gifts { get; set; } = Enumerable.Empty<GiftVoteViewModel>();
        public int? GiftVoteId { get; set; }
        public ApplicationUser Employee { get; set; } = null!;
    }
}
