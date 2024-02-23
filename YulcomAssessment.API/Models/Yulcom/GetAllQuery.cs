using MediatR;
using YulcomAssesment.API.Models.Response;

namespace YulcomAssesment.API.Models.Yulcom
{
    public class GetAllQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
