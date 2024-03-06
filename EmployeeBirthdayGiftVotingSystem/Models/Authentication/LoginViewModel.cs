using EmployeeBirthdayGiftVotingSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeBirthdayGiftVotingSystem.Models.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = UserValidationErrorMessages.Username.Required)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = UserValidationErrorMessages.Password.Required)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
