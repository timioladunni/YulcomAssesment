using MediatR;
using YulcomAssesment.API.Models.Response;

namespace YulcomAssesment.API.Models.Yulcom
{
    public class GetByIdQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
