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
    public class ElseController : ControllerBase
    {
        private readonly ISomethingElseCreateInteractor createInteractor;
        private readonly ISomethingElseReadInteractor readInteractor;
        private readonly ISomethingElseUpdateInteractor updateInteractor;
        private readonly ISomethingElseDeleteInteractor deleteInteractor;
        private readonly ILogger logger;

        public ElseController(ISomethingElseCreateInteractor createInteractor, ISomethingElseReadInteractor readInteractor, ISomethingElseUpdateInteractor updateInteractor, ISomethingElseDeleteInteractor deleteInteractor, ILogger logger)
        {
            this.createInteractor = createInteractor;
            this.readInteractor = readInteractor;
            this.updateInteractor = updateInteractor;
            this.deleteInteractor = deleteInteractor;
            this.logger = logger;
        }
        [HttpPost]
        [Route("api/thingselse")]
        public async Task<IActionResult> CreateElseAsync([FromForm] string name, [FromForm] string[] othername)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (name.Length < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            try
            {
                await createInteractor.CreateSomethingElseAsync(name, othername);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Route("api/thingselse/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromForm] string othername)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (othername.Length < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

            if (id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            try
            {
                await updateInteractor.UpdateSomethingElseAddSomethingAsync(id, othername);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("api/thingselse/{else_id}/{something_id}")]
        public async Task<ActionResult> DeleteAsync(int else_id, int something_id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (else_id < 1 || something_id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            
            try
            {
                await updateInteractor.UpdateSomethingElseDeleteSomethingAsync(else_id, something_id);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        [Route("api/thingselse/{else_id}")]
        public async Task<ActionResult> DeleteAsync(int else_id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (else_id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            try
            {
                await deleteInteractor.DeleteSomethingElseAsync(else_id);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        [Route("api/thingselse")]
        public async Task<ActionResult> GetElseListAsync()
        {            
            try
            {
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [MapToApiVersion("2.0")]
        [HttpPatch]
        [Route("api/thingselse/tag/{id}")]
        public async Task<ActionResult> PatchTagAsync(int id, [FromForm] string tag)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            try
            {
                await updateInteractor.UpdateSomethingElseChangeTagAsync(id, tag);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [MapToApiVersion("2.0")]
        [HttpPatch]
        [Route("api/thingselse/things/{id}")]
        public async Task<ActionResult> PatchSomethingAsync(int id, [FromForm] string othername)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (othername.Length < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

            if (id < 1)
                try
                {
                    return await GetAllSomethingElseIncludeSomethingAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            try
            {
                await updateInteractor.UpdateSomethingElseAddSomethingAsync(id, othername);
                return await GetAllSomethingElseIncludeSomethingAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        private async Task<ActionResult> GetAllSomethingElseIncludeSomethingAsync()
        {
            try
            {
                var result = await readInteractor.GetSomethingElseIncludingSomethingsListAsync();
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
