using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TempoAplicativo.Data.Entities;
using TempoAplicativo.Helpers;
using TempoAplicativo.Models;

namespace TempoAplicativo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public AccountController(UserManager<User> userManager, IUserHelper userHelper, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _userHelper = userHelper;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.Email,
                        Email = model.Email,
                        Password = model.Password,

                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);


                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "Error when creating account");
                        return View(model);
                    }

                    ViewBag.Message = "Account Created ! :D";
                }

            }
            return View(model);
        }

    }
}
