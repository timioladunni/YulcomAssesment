using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YulcomAssesment.API.Models.Application;
using YulcomAssesment.API.Response;
using YulcomAssesment.API.Services.Interface;

namespace YulcomAssesment.API.Controllers
{
    [Authorize]
    [Route("[controller]/api/v1")]
    [ApiController]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService applicationService;
        private readonly ILogger<ApplicationController> logger;
        public ApplicationController(IApplicationService applicationService, ILogger<ApplicationController> logger)
        {
            this.applicationService = applicationService;
            this.logger = logger;
        }

        /// <summary>
        /// This Endpoint Generates JWT Token for Authenticating the API Endpoints
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("generatetoken")]
        public async Task<IActionResult> GenerateTokenAsync([FromForm] GenerateTokenRequestDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(" | ", ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage));
                    return BadRequest(ReturnedResponse.ModelStateResponse(errMessage, null));
                }

                var resp = await applicationService.GenerateTokenAsync(model);

                if (resp.Message == ResponseStatus.Successful.ToString())
                    return Ok(resp);
                else
                    return BadRequest(resp);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(GenerateTokenAsync)} EndPoint ", errMessage));
                return BadRequest(ReturnedResponse.ErrorResponse(errMessage, null));
            }
        }
    }
}
