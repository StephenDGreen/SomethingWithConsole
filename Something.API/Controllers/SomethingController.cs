using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Something.Application;
using System;
using System.Threading.Tasks;

namespace Something.API.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class SomethingController : ControllerBase
    {

        private readonly ISomethingCreateInteractor createInteractor;
        private readonly ISomethingReadInteractor readInteractor;
        private readonly ILogger logger;

        public SomethingController(ISomethingCreateInteractor createInteractor, ISomethingReadInteractor readInteractor, ILogger logger)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/things")]
        public async Task<ActionResult> CreateAsync([FromForm] string name)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (name == null)
                return BadRequest();
            if (name.Length < 1)
            {
                try
                {
                    return await GetAllAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            try
            {
                createInteractor.CreateSomething(name);
                return await GetAllAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpGet]
        [Route("api/things")]
        public async Task<ActionResult> GetListAsync()
        {
            try
            {
                return await GetAllAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        private async Task<ActionResult> GetAllAsync()
        {            
            try
            {
                var result = await readInteractor.GetSomethingListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                throw;
            }
        }
    }
}
