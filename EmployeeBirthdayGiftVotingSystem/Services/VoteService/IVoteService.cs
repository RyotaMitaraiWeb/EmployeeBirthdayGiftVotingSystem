using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Models.Vote;
namespace EmployeeBirthdayGiftVotingSystem.Services.VoteService
{
    public interface IVoteService
    {
        Task<int?> CreateVote(CreateVoteViewModel vote, string creatorId, DateTime today);
        Task<int?> EndVote(string creatorId, int birthdayVoteId);
        Task<int?> CastVote(string userId, int voteId, int giftId);
        Task<IEnumerable<CreateVoteViewModel>> GetAllUsersThatCanHaveAVote(string creatorId, DateTime today);
        Task<IEnumerable<VoteIndexViewModel>> GetVotesIndexList(string currentUserId);
        Task<BirthdayVote?> GetVoteDetails(int id);
    }
}
