using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeBirthdayGiftVotingSystem.Data.Seeders
{
    public class ApplicationUserSeeder : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser()
                {
                    Id = Guid.Parse("8018e901-3aa6-4345-8675-fadbb6852c7b"),
                    UserName = "therealjohn",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = this._passwordHasher.HashPassword(null!, "123456"),
                    NormalizedUserName = "THEREALJOHN",
                    Birthday = new DateTime(year: 1999, month: 1, day: 1).ToUniversalTime(),
                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("6dc922b1-3987-4a34-83ec-c8b27a718fbb"),
                    UserName = "therealjane",
                    FirstName = "Jane",
                    LastName = "Doe",
                    PasswordHash = this._passwordHasher.HashPassword(null!, "654321"),
                    NormalizedUserName = "THEREALJANE",
                    Birthday = new DateTime(year: 1999, month: 1, day: 2).ToUniversalTime(),
                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("29506ae4-eccc-47d8-94ed-ec6ffc8023c5"),
                    UserName = "Alakazam",
                    FirstName = "Henry",
                    LastName = "Wilson",
                    PasswordHash = this._passwordHasher.HashPassword(null!, "abrakadabra"),
                    NormalizedUserName = "ALAKAZAM",
                    Birthday = new DateTime(year: 1990, month: 4, day: 5).ToUniversalTime(),
                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("1976a0d1-d843-4c6a-a746-1d909178d1de"),
                    UserName = "lee",
                    FirstName = "Lee",
                    LastName = "Everett",
                    PasswordHash = this._passwordHasher.HashPassword(null!, "password"),
                    NormalizedUserName = "LEE",
                    Birthday = new DateTime(year: 1984, month: 7, day: 7).ToUniversalTime(),
                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("4e592c87-0e1f-4b64-97f2-31aa0444705d"),
                    UserName = "ryota1",
                    FirstName = "Ryota",
                    LastName = "Mitarai",
                    PasswordHash = this._passwordHasher.HashPassword(null!, "abcde"),
                    NormalizedUserName = "RYOTA1",
                    Birthday = new DateTime(year: 2002, month: 6, day: 4).ToUniversalTime(),
                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("a6795017-baf4-477f-b289-fbf01e755dd8"),
                    UserName = "texas",
                    FirstName = "Joel",
                    LastName = "Miller",
                    PasswordHash = this._passwordHasher.HashPassword(null!, "austin"),
                    NormalizedUserName = "TEXAS",
                    Birthday = new DateTime(year: 1981, month: 9, day: 27).ToUniversalTime(),
                }
            );
        }

        private readonly PasswordHasher<ApplicationUser> _passwordHasher = new();
    }
}
