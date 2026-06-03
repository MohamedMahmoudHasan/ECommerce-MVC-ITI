using ECommerce.BLL;
using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountManager accountManager, SignInManager<ApplicationUser> signInManager)
        {
            _accountManager = accountManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterMV registerMV)
        {
            if (!ModelState.IsValid)
            {
                return View(registerMV);
            }

            IdentityResult result = await _accountManager.RegisterAsync(registerMV);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(registerMV);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginMV loginMV)
        {
            if (!ModelState.IsValid)
            {
                return View(loginMV);
            }
            var user = await _accountManager.FindByEmailAsync(loginMV.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email Or Password");
                return View(loginMV);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginMV.Password, loginMV.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email Or Password");
                return View(loginMV);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
