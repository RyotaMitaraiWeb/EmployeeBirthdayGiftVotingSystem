using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Models.Gift;

namespace EmployeeBirthdayGiftVotingSystem.Models.Vote
{
    public class VoteDetailsViewModel
    {
        public int VoteId { get; set; }
        public IEnumerable<GiftVoteViewModel>? Gifts { get; set; }
        public int? GiftVoteId { get; set; }
        public ApplicationUser Employee { get; set; } = null!;
        public Dictionary<string, IEnumerable<string>>? UserGiftVotes { get; set; }
    }
}
