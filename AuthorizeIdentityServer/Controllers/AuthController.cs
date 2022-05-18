using AuthorizeIdentityServer.Models;
using AuthorizeIdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthorizeIdentityServer.Controllers
{
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        public AuthController( UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }  

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                user  = new AppUser { UserName = model.UserName };
                var res = await _userManager.CreateAsync(user, model.Password);

                if (res.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role , "DefaultRole"));
                }

            }

            var SignInRes  =  await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (SignInRes.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("UserName","Something went wrong");

            return View(model);
        }

        [Route("[action]")]
        public IActionResult Registration(string returnUrl)
        {
            return View();
        }
    }
}
