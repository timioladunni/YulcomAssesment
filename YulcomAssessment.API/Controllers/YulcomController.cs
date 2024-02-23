using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using YulcomAssesment.API.Models.Yulcom;
using Microsoft.AspNetCore.Authorization;
using YulcomAssesment.API.Response;
using YulcomAssesment.API.Extensions.ActionFilters;

namespace YulcomAssesment.API.Controllers
{
    [Authorize]
    [TypeFilter(typeof(AuditLogFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class YulcomController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ApplicationController> logger;

        public YulcomController(IMediator mediator, ILogger<ApplicationController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Endpoint To Create a New Record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody] YulcomModel model)
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

                var command = new CreateYulcomCommand { CreateEntity = model };

                var resp = await mediator.Send(command);

                if (resp.Message == ResponseStatus.Successful.ToString())
                    return Ok(resp);
                else
                    return BadRequest(resp);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(CreateAsync)} EndPoint ", errMessage));
                return BadRequest(ReturnedResponse.ErrorResponse(errMessage, null));
            }
        }

        /// <summary>
        /// Endpoint To Update Exsiting Record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] YulcomModel model)
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

                var command = new UpdateYulcomCommand { Id = id,  UpdateEntity = model };

                var resp = await mediator.Send(command);

                if (resp.Message == ResponseStatus.Successful.ToString())
                    return Ok(resp);
                else
                    return BadRequest(resp);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(UpdateAsync)} EndPoint ", errMessage));
                return BadRequest(ReturnedResponse.ErrorResponse(errMessage, null));
            }
        }


        /// <summary>
        /// Endpoint To Delete Exsiting Record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var command = new DeleteYulcomCommand { Id = id };
                var resp = await mediator.Send(command);

                if (resp.Message == ResponseStatus.Successful.ToString())
                    return Ok(resp);
                else
                    return BadRequest(resp);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(DeleteAsync)} EndPoint ", errMessage));
                return BadRequest(ReturnedResponse.ErrorResponse(errMessage, null));
            }
        }

        /// <summary>
        /// Endpoint To Get Exsiting Record By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var query = new GetByIdQuery { Id = id };
                var resp = await mediator.Send(query);

                if (resp.Message == ResponseStatus.Successful.ToString())
                    return Ok(resp);
                else
                    return BadRequest(resp);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(GetByIdAsync)} EndPoint ", errMessage));
                return BadRequest(ReturnedResponse.ErrorResponse(errMessage, null));
            }
        }

        /// <summary>
        /// Paginated Endpoint To get All Records
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = new GetAllQuery { PageNumber = pageNumber, PageSize = pageSize };
                var resp = await mediator.Send(query);

                if (resp.Message == ResponseStatus.Successful.ToString())
                    return Ok(resp);
                else
                    return BadRequest(resp);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(GetAllAsync)} EndPoint ", errMessage));
                return BadRequest(ReturnedResponse.ErrorResponse(errMessage, null));
            }

        }
    }

}
