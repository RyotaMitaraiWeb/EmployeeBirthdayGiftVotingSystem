using System.ComponentModel.DataAnnotations;

namespace EmployeeBirthdayGiftVotingSystem.Data.Entities
{
    public class Gift
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Examples: "toothbrush.jpg", "black_dice.png", etc.
        /// </summary>
        [Required]
        public string ImageFileName { get; set; } = string.Empty;
    }
}
