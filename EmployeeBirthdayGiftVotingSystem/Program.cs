using EmployeeBirthdayGiftVotingSystem.Contracts;
using EmployeeBirthdayGiftVotingSystem.Data;
using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Data.Repositories;
using EmployeeBirthdayGiftVotingSystem.Services.GiftService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EmployeeBirthdayGiftVotingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var dbConnection = new NpgsqlConnectionStringBuilder()
            {
                Host = builder.Configuration["DB_HOST"],
                Database = builder.Configuration["DB_NAME"],
                Username = builder.Configuration["DB_USER"],
                Password = builder.Configuration["DB_PASSWORD"],
            };

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(dbConnection.ConnectionString);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IGiftService, GiftService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
