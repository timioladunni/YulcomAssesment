using YulcomAssesment.API.Models.Application;
using YulcomAssesment.API.Models.Response;
using YulcomAssesment.API.Services.Implementation;

namespace YulcomAssesment.API.Services.Interface
{
    public interface IApplicationService
    {
        Task<ApiResponse> GenerateTokenAsync(GenerateTokenRequestDto model);
    }
}
