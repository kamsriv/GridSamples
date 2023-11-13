using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VFSample.Controllers
{
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
           return RedirectToAction("GenerateToken", new { un = Request.Query["u"], pd = Request.Query["p"] });
           //return $"TOKEN - {HttpContext.User?.Identity?.Name} - {re}";
        }
        [Route("GenerateToken")]
        public IActionResult GenerateToken(string un, string pd)
        {
            //session generation will be done here.
            HttpContext.Session.SetInt32("session::started", 1);
            return Ok(new { u = un, p = pd });
        }
    }
}
