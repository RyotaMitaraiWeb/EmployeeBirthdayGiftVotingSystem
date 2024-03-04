using System.ComponentModel.DataAnnotations;

namespace EmployeeBirthdayGiftVotingSystem.Models.Vote
{
    public class VoteIndexViewModel
    {
        public int Id { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime EmployeeBirthday { get; set; }
    }
}
