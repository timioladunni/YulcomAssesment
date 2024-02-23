using MediatR;
using YulcomAssesment.API.Models.Response;

namespace YulcomAssesment.API.Models.Yulcom
{
    public class CreateYulcomCommand : IRequest<ApiResponse>
    {
        public YulcomModel CreateEntity { get; set; }
    }
}
