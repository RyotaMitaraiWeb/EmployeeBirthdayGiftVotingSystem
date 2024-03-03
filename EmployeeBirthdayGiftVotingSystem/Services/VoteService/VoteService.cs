﻿using EmployeeBirthdayGiftVotingSystem.Contracts;
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

        public Task<int?> CastVote(string userId, int voteId, int giftId)
        {
            throw new NotImplementedException();
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

        public Task<int?> EndVote(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CreateVoteListViewModel>> GetAllUsersThatCanHaveAVote(string creatorId, DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}
