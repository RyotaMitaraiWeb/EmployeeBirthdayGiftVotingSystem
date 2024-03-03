using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeBirthdayGiftVotingSystem.Data.Entities
{
    public class BirthdayVote
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public int Year { get; set; }

        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public ApplicationUser Employee { get; set; } = null!;

        public Guid CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser Creator { get; set; } = null!;

        public List<UserGiftVote> UserGiftVotes { get; set; } = [];
    }
}
