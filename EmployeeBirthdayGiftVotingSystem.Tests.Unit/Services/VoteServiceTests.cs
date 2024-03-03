using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Models.Vote;
using EmployeeBirthdayGiftVotingSystem.Services.VoteService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.NSubstitute;
using NSubstitute;
using Tests.Unit;


namespace EmployeeBirthdayGiftVotingSystem.Tests.Unit.Services
{
    public class VoteServiceTests
    {
        public IRepository Repository { get; set; } = Substitute.For<IRepository>();
        public VoteService VoteService { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; } = UserManagerMock.Create();
        private readonly IQueryable<ApplicationUser> Users = new List<ApplicationUser>()
        {
            new() { Id = Guid.NewGuid(), UserName = "a", FirstName = "aa", LastName = "aaa", Birthday = new DateTime(2005, 2, 2).ToUniversalTime() },
            new() { Id = Guid.NewGuid(), UserName = "b", FirstName = "bb", LastName = "bbb", Birthday = new DateTime(2005, 2, 2).ToUniversalTime() },
            new() { Id = Guid.NewGuid(), UserName = "c", FirstName = "cc", LastName = "ccc", Birthday = new DateTime(2005, 2, 2).ToUniversalTime() },
            new() { Id = Guid.NewGuid(), UserName = "d", FirstName = "dd", LastName = "ddd", Birthday = new DateTime(2005, 2, 2).ToUniversalTime() },
            new() { Id = Guid.NewGuid(), UserName = "e", FirstName = "ee", LastName = "eee", Birthday = new DateTime(2005, 2, 2).ToUniversalTime() },
        }.AsQueryable();

        [SetUp]
        public void SetUp()
        {
            this.VoteService = new VoteService(this.Repository, this.UserManager);
        }

        [Test]
        public async Task Test_CreateVoteReturnsANonNullValueWhenSuccessful()
        {
            var employee = this.Users.First();
            var creator = this.Users.First(u => u.UserName == "b");

            var vote = new CreateVoteViewModel()
            {
                EmployeeId = employee.Id.ToString(),
            };

            this.UserManager.FindByIdAsync(vote.EmployeeId).Returns(employee);

            var votes = new List<BirthdayVote>()
            { new() { Id = 1, EmployeeId = employee.Id, IsActive = false, Year = 2021 } };

            this.Repository.AllReadonly<BirthdayVote>().Returns(votes.BuildMock());

            this.UserManager.Users.Returns(this.Users);
            this.Repository.SaveChangesAsync().Returns(1);
          
            var result = await this.VoteService.CreateVote(vote, creator.Id.ToString(), new DateTime(2022, 4, 4));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Test_CreateVoteReturnsNullWhenTheEmployeeDoesNotExist()
        {
            var creator = this.Users.First(u => u.UserName == "b");
            ApplicationUser? employee = null;
            var vote = new CreateVoteViewModel()
            {
                EmployeeId = "1",
            };

            this.UserManager.FindByIdAsync("1").Returns(employee);
            var result = await this.VoteService.CreateVote(vote, creator.Id.ToString(), new DateTime());
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_CreateVoteReturnsNullWhenTheCreatorTriesToCreateAVoteForThemself()
        {
            var creator = this.Users.First();
            ApplicationUser employee = creator;
            var vote = new CreateVoteViewModel()
            {
                EmployeeId = Guid.NewGuid().ToString(),
            };

            this.UserManager.FindByIdAsync(creator.Id.ToString()).Returns(employee);
            var result = await this.VoteService.CreateVote(vote, creator.Id.ToString(), new DateTime());
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_CreateVoteReturnsNullWhenTheEmployeeHasAnActiveVoting()
        {
            var employee = this.Users.First();
            var creator = this.Users.First(u => u.UserName == "b");

            var vote = new CreateVoteViewModel()
            {
                EmployeeId = employee.Id.ToString(),
            };

            this.UserManager.FindByIdAsync(vote.EmployeeId).Returns(employee);

            var votes = new List<BirthdayVote>()
            { new() { Id = 1, EmployeeId = employee.Id, IsActive = true, Year = 2021 } };

            this.Repository.AllReadonly<BirthdayVote>().Returns(votes.BuildMock());

            var result = await this.VoteService.CreateVote(vote, creator.Id.ToString(), new DateTime());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Test_CreateVoteReturnsNullWhenTheEmployeHasAlreadyHadAVoteThatYear()
        {
            var employee = this.Users.First();
            var creator = this.Users.First(u => u.UserName == "b");

            var vote = new CreateVoteViewModel()
            {
                EmployeeId = employee.Id.ToString(),
            };

            this.UserManager.FindByIdAsync(vote.EmployeeId).Returns(employee);

            var votes = new List<BirthdayVote>()
            { new() { Id = 1, EmployeeId = employee.Id, IsActive = false, Year = 2021 } };

            this.Repository.AllReadonly<BirthdayVote>().Returns(votes.BuildMock());

            var result = await this.VoteService.CreateVote(vote, creator.Id.ToString(), new DateTime(2021, 5, 5));

            Assert.That(result, Is.Null);
        }
    }
}
