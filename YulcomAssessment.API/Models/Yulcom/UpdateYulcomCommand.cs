using MediatR;
using YulcomAssesment.API.Models.Response;

namespace YulcomAssesment.API.Models.Yulcom
{
    public class UpdateYulcomCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public YulcomModel UpdateEntity { get; set; }
    }
}
