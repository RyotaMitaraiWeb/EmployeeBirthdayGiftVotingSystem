namespace EmployeeBirthdayGiftVotingSystem.Models.Vote
{
    public class CreateVoteListViewModel
    {
        public string EmployeeId { get; set; } = string.Empty;
        public IEnumerable<CreateVoteViewModel> AvailableUsers { get; set; } = [];
    }
}
