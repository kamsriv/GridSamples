using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult GenerateToken([Required]string un,[Required] string pd)
        {
            //PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain);//Connecting to local computer.
            
            //Connecting to Active Directory
            PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Machine, "NEEV", 
                                                   un, pd);

            if (UserPrincipal.FindByIdentity(insPrincipalContext, un) != null)
            {
                WindowsIdentity identity = new WindowsIdentity(UserPrincipal.FindByIdentity(insPrincipalContext, un).UserPrincipalName);
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                HttpContext.User = principal;
                //session generation will be done here.
                HttpContext.Session.SetInt32("session::started", 1);
                return Ok(new { u = un, p = pd });
            }
            return BadRequest();
        }
    }
}
