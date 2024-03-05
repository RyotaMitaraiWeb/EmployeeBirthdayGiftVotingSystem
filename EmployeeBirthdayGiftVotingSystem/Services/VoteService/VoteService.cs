using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Models.Vote;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBirthdayGiftVotingSystem.Services.VoteService
{
    public class VoteService(IRepository repository, UserManager<ApplicationUser> userManager) : IVoteService
    {
        private readonly IRepository _repository = repository;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<int?> CastVote(string userId, int voteId, int giftId)
        {
            var userVote = await this._repository
                .All<UserGiftVote>()
                .Where(ugv => Guid.Equals(Guid.Parse(userId), ugv.VoterId)
                        && ugv.BirthdayVoteId == voteId
                        && ugv.GiftId == null
                        && ugv.BirthdayVote.IsActive
                    )
                .FirstOrDefaultAsync();

            if (userVote == null)
            {
                return null;
            }

            userVote.GiftId = giftId;
            await this._repository.SaveChangesAsync();
            return userVote.Id;
        }

        public async Task<int?> CreateVote(CreateVoteViewModel vote, string creatorId, DateTime today)
        {
            ApplicationUser? employee = await this._userManager.FindByIdAsync(vote.EmployeeId);
            if (employee == null)
            {
                return null;
            }

            if (employee.Id.Equals(Guid.Parse(creatorId)))
            {
                return null;
            }

            BirthdayVote? employeeCurrentVote = await this._repository.AllReadonly<BirthdayVote>()
                .Where(bv => (bv.IsActive || bv.Year == today.Year) && bv.EmployeeId.Equals(employee.Id))
                .FirstOrDefaultAsync();

            if (employeeCurrentVote != null)
            {
                return null;
            }

            IEnumerable<ApplicationUser> voters = this._userManager.Users.Where(u => u.NormalizedUserName != employee.NormalizedUserName);

            BirthdayVote birthdayVote = new()
            {
                IsActive = true,
                Year = today.Year,
                EmployeeId = employee.Id,
                Employee = employee,
                CreatorId = Guid.Parse(creatorId),
            };
            
            List<UserGiftVote> votes = voters.Select(v => new UserGiftVote()
            {
                VoterId = v.Id,
                GiftId = null,

            }).ToList();

            birthdayVote.UserGiftVotes = votes;

            await this._repository.AddAsync(birthdayVote);
            await this._repository.SaveChangesAsync();
            return birthdayVote.Id;
        }

        public async Task<int?> EndVote(string creatorId, int birthdayVoteId)
        {
            var birthdayVote = await this._repository.GetByIdAsync<BirthdayVote>(birthdayVoteId);
            if (birthdayVote == null)
            {
                return null;
            }

            if (!birthdayVote.IsActive || !Guid.Equals(birthdayVote.CreatorId, Guid.Parse(creatorId)))
            {
                return null;
            }

            birthdayVote.IsActive = false;
            await this._repository.SaveChangesAsync();

            return birthdayVote.Id;
        }

        public async Task<IEnumerable<VoteIndexViewModel>> GetVotesIndexList(string currentUserId
            )
        {
            Guid id = Guid.Parse(currentUserId);
            var users = await this._repository
                .AllReadonly<BirthdayVote>()
                .Where(bv => !bv.EmployeeId.Equals(id))
                .Select(bv => new VoteIndexViewModel()
                {
                    Id = bv.Id,
                    CreatorId = bv.CreatorId.ToString(),
                    FirstName = bv.Creator.FirstName!,
                    LastName = bv.Creator.LastName!,
                    Username = bv.Creator.UserName!,
                    IsActive = bv.IsActive,
                    EmployeeBirthday = bv.Creator.Birthday,
                })
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<CreateVoteViewModel>> GetAllUsersThatCanHaveAVote(string creatorId, DateTime today)
        {
            var users = await this._userManager.Users
                .Where(u =>
                    !u.Id.Equals(Guid.Parse(creatorId)) && u.BirthdayVotes.All(bv => !bv.IsActive && bv.Year < today.Year))
                .Select(u => new CreateVoteViewModel()
                {
                    EmployeeId = u.Id.ToString(),
                    FirstName = u.FirstName!,
                    LastName = u.LastName!,
                    Username = u.UserName!,
                })
                .ToListAsync();
            return users;
        }

        public async Task<BirthdayVote?> GetVoteDetails(int id)
        {
            return await this._repository.AllReadonly<BirthdayVote>()
                .Include(bv => bv.Employee)
                .Include(bv => bv.UserGiftVotes)
                .ThenInclude(ugv => ugv.Gift)
                .Include(bv => bv.UserGiftVotes)
                .ThenInclude(ugv => ugv.Voter)
                .FirstOrDefaultAsync(bv => bv.Id == id);
        }
    }
}
