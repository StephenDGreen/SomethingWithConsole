using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Something.Security;
using System;

namespace Something.API.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class HomeController : ControllerBase
    {
        private readonly ISomethingUserManager userManager;
        private readonly ILogger logger;

        public HomeController(ISomethingUserManager userManager, ILogger logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        [AllowAnonymous]
        [Route("home/authenticate")]
        public ActionResult Authenticate()
        {
            try
            {
                var token = userManager.GetUserToken();
                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
    }
}
