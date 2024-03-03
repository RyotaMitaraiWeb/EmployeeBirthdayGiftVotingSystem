using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Models.Vote;
namespace EmployeeBirthdayGiftVotingSystem.Services.VoteService
{
    public interface IVoteService
    {
        Task<int?> CreateVote(CreateVoteViewModel vote, string creatorId, DateTime today);
        Task<int?> EndVote(Guid userId);
        Task<int?> CastVote(string userId, int voteId, int giftId);
        Task<IEnumerable<CreateVoteViewModel>> GetAllUsersThatCanHaveAVote(string creatorId, DateTime today);

    }
}
