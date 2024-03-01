using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Models.Gift;

namespace EmployeeBirthdayGiftVotingSystem.Contracts
{
    public interface IGiftService
    {
        Task<bool> CheckIfGiftExists(int id);
        Task<IEnumerable<AllGiftsViewModel>> GetAllGifts();
    }
}
