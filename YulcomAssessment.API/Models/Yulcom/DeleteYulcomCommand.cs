using MediatR;
using YulcomAssesment.API.Models.Response;

namespace YulcomAssesment.API.Models.Yulcom
{
    public class DeleteYulcomCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
