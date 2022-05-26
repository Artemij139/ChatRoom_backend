using AuthorizeIdentityServer.Models;
using AuthorizeIdentityServer.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthorizeIdentityServer.Controllers
{
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        public AuthController( UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService identityServerInteractionService)
        {   
            _userManager = userManager;
            _signInManager = signInManager;
            _interactionService = identityServerInteractionService;
        }

        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        private readonly IIdentityServerInteractionService _interactionService;

        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
            });
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
                ModelState.AddModelError("CustomError", "Пользователь не найден!");
                return View(model);
            }

            var SignInRes  =  await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (SignInRes.Succeeded)
            {   
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("CustomError", "Что то пошло не так...");

            return View(model);
        }

        [Route("[action]")]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser { UserName = model.UserName };

            var res = await _userManager.CreateAsync(user, model.Password);
            
            if (res.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "DefaultRole"));
            }
            else
            {
                ModelState.AddModelError("CustomError", "Не удалось зарегистрировать пользователя!");
                return View(model);
            }

            var SignInRes = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (SignInRes.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("CustomError", "Что то пошло не так...");

            return View(model);
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRes =  await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(logoutRes.PostLogoutRedirectUri);
        }

    }
}
