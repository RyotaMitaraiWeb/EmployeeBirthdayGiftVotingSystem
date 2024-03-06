using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Models.Gift;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBirthdayGiftVotingSystem.Services.GiftService
{
    public class GiftService(IRepository repository) : IGiftService
    {
        private readonly IRepository _repository = repository;

        public async Task<bool> CheckIfGiftExists(int id)
        {
            var gift = await this._repository.GetByIdAsync<Gift>(id);
            return gift != null;
        }

        public async Task<IEnumerable<AllGiftsViewModel>> GetAllGifts()
        {
            return await this._repository.AllReadonly<Gift>()
                .OrderByDescending(g => g.Id)
                .Select(g => new AllGiftsViewModel()
                {
                    Name = g.Name,
                    Description = g.Description,
                    ImageFileName = g.ImageFileName,
                }).ToListAsync();
        }

        public async Task<IEnumerable<GiftVoteViewModel>> GetGiftsForVoting()
        {
            return await this._repository.AllReadonly<Gift>().Select(g => new GiftVoteViewModel()
            {
                Id = g.Id,
                Name = g.Name,
            }).ToListAsync();
        }
    }
}
