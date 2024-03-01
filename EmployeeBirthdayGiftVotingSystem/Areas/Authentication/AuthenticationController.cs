using EmployeeBirthdayGiftVotingSystem.Data.Entities.Identity;
using EmployeeBirthdayGiftVotingSystem.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBirthdayGiftVotingSystem.Areas.Authentication
{
    public class AuthenticationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private const string FailedLogin = "Wrong username or password!";

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    this.AddFailedLoginError();
                    return View(model);
                }

                bool isLoggedIn = await this._userManager.CheckPasswordAsync(user, model.Password);
                if (!isLoggedIn)
                {
                    this.AddFailedLoginError();
                    return View(model);
                }

                await this._signInManager.SignInAsync(user, isPersistent: true);
                return Redirect("/Home/Index");
            }

            return View(model);
        }

        private void AddFailedLoginError()
        {
            this.ViewData["FailedLogin"] = "Wrong username or password!";
        }
    }
}
