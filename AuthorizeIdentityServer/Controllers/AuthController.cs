using AuthorizeIdentityServer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizeIdentityServer.Controllers
{
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        [Route("[action]")]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginViewModel model)
        {
            return View();   
        }
    }
}
