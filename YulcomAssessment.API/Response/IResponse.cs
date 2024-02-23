using Microsoft.AspNetCore.Mvc;

namespace YulcomAssesment.API.Response
{
    public interface IResponse
    {
        IActionResult ReturnResponse(string response);
    }
}
