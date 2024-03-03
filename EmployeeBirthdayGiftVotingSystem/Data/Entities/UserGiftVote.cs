using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeBirthdayGiftVotingSystem.Data.Entities
{
    public class UserGiftVote
    {
        public int Id { get; set; }

        public Guid VoterId { get; set; }

        [ForeignKey(nameof(VoterId))]
        public ApplicationUser Voter { get; set; } = null!;

        /// <summary>
        /// If 0, the user has not voted yet
        /// </summary>
        public int? GiftId { get; set; }

        [ForeignKey(nameof(GiftId))]
        public Gift? Gift { get; set; }

        public int BirthdayVoteId { get; set; }

        [ForeignKey(nameof(BirthdayVoteId))]
        public BirthdayVote BirthdayVote { get; set; } = null!;
    }
}
