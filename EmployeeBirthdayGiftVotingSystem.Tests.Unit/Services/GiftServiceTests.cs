using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Services.GiftService;
using NSubstitute;

namespace EmployeeBirthdayGiftVotingSystem.Tests.Unit.Services
{
    public class GiftServiceTests
    {
        public IRepository Repository = Substitute.For<IRepository>();
        public GiftService GiftService { get; set; }

        [SetUp]
        public void SetUp()
        {
            this.GiftService = new GiftService(this.Repository);
        }

        [Test]
        public async Task Test_CheckIfGiftExistsReturnsTrueWhenItFindsTheGift()
        {
            this.Repository.GetByIdAsync<Gift>(1).Returns(new Gift());

            var result = await this.GiftService.CheckIfGiftExists(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Test_CheckIfGiftExistsReturnsFalseWhenItFailsToFindTheGift()
        {
            Gift? gift = null;
            this.Repository.GetByIdAsync<Gift>(1).Returns(gift);

            var result = await this.GiftService.CheckIfGiftExists(1);

            Assert.That(result, Is.False);
        }
    }
}
