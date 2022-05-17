using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public string returnSecret()
        {
            return "this is Sectret Bitch!";
        }
    }
}

