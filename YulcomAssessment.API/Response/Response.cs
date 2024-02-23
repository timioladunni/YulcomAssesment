using YulcomAssesment.API.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace YulcomAssesment.API.Response
{
    public class SuccessResponse : IResponse
    {
        public IActionResult ReturnResponse(string response)
        {
            ApiResponse successResp = new() { Message = "Successful", Data = JsonConvert.DeserializeObject(response), Error = null };
            return new OkObjectResult(successResp);
        }
    }

    public static class ReturnedResponse
    {
        public static ApiResponse ErrorResponse(string message, object data)
        {
            var apiResp = new ApiResponse();
            apiResp.Status = false;
            apiResp.Data = data;
            apiResp.Message = ResponseStatus.Unsuccessful.ToString();
            apiResp.Code = "400";
            var error = new ApiError();
            error.Message = message;
            error.Code = 400;
            apiResp.Error = error;

            return apiResp;
        }

        public static ApiResponse SuccessResponse(string message, object data)
        {
            var apiResp = new ApiResponse();
            apiResp.Status = true;
            apiResp.Data = data;
            apiResp.Message = ResponseStatus.Successful.ToString();
            apiResp.Code = "200";
            var error = new ApiError();
            return apiResp;
        }

        public static ApiResponse ModelStateResponse(string message, object data)
        {
            var apiResp = new ApiResponse();
            apiResp.Status = false;
            apiResp.Data = data;
            apiResp.Message = ResponseStatus.Unsuccessful.ToString();
            apiResp.Code = "400";
            var error = new ApiError();
            error.Message = message;
            apiResp.Error = error;

            return apiResp;
        }
    }
}
