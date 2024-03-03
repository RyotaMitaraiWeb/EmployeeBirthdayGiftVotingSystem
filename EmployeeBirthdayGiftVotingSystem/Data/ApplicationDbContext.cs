using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBirthdayGiftVotingSystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.BirthdayVotes)
                .WithOne(bv => bv.Employee)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BirthdayVote>()
                .HasOne(bv => bv.Creator)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.ApplyConfiguration(new ApplicationUserSeeder());
            builder.ApplyConfiguration(new GiftSeeder());
        }

        public DbSet<UserGiftVote> UserGiftVotes { get; set; }
        public DbSet<BirthdayVote> BirthdayVotes { get; set; }
    }
}
