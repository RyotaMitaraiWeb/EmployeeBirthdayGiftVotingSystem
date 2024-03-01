using EmployeeBirthdayGiftVotingSystem.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        [Required]
        [MaxLength(UserValidationRules.FirstName.MaxLength)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(UserValidationRules.LastName.MaxLength)]
        public string? LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
