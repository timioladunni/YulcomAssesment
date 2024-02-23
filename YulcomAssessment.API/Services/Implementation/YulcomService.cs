using MediatR;
using Microsoft.EntityFrameworkCore;
using YulcomAssesment.API.Controllers;
using YulcomAssesment.API.Data;
using YulcomAssesment.API.Mappers;
using YulcomAssesment.API.Models.Response;
using YulcomAssesment.API.Models.Yulcom;
using YulcomAssesment.API.Response;

namespace YulcomAssesment.API.Services.Implementation
{
    public class YulcomHandlerService : IRequestHandler<CreateYulcomCommand, ApiResponse>, IRequestHandler<UpdateYulcomCommand, ApiResponse>,
                                        IRequestHandler<DeleteYulcomCommand, ApiResponse>, IRequestHandler<GetByIdQuery, ApiResponse>,IRequestHandler<GetAllQuery, ApiResponse>
    {
        private readonly YulcomAssesmentContext yulcomContext;
        private readonly ILogger<YulcomHandlerService> logger;
        public YulcomHandlerService(YulcomAssesmentContext yulcomContext, ILogger<YulcomHandlerService> logger)
        {
            this.yulcomContext = yulcomContext ?? throw new ArgumentNullException(nameof(yulcomContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse> Handle(CreateYulcomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await yulcomContext.YulcomAssesmentData.AddAsync(request.CreateEntity.AsDto());
                await yulcomContext.SaveChangesAsync(cancellationToken);
                return ReturnedResponse.SuccessResponse(null, "Succesfully Created Entry");
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(CreateYulcomCommand)} Service ", errMessage));
                return ReturnedResponse.ErrorResponse($"Any Error Occured Processing Request", null);
            }
        }

        public async Task<ApiResponse> Handle(UpdateYulcomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var yulcom = await yulcomContext.YulcomAssesmentData.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (yulcom is null) return ReturnedResponse.ErrorResponse("Id does not exist", null);

                yulcom.Name = request.UpdateEntity.Name;
                yulcom.Type1 = request.UpdateEntity.Type1;
                yulcom.Type2 = request.UpdateEntity.Type2;
                yulcom.Total = request.UpdateEntity.Total;
                yulcom.HP = request.UpdateEntity.HP;
                yulcom.Attack = request.UpdateEntity.Attack;
                yulcom.Defense = request.UpdateEntity.Defense;
                yulcom.SpAttack = request.UpdateEntity.SpAttack;
                yulcom.SpDefense = request.UpdateEntity.SpDefense;
                yulcom.Speed = request.UpdateEntity.Speed;
                yulcom.Generation = request.UpdateEntity.Generation;
                yulcom.Legendary = request.UpdateEntity.Legendary;

                yulcomContext.Entry(yulcom).State = EntityState.Modified;

                await yulcomContext.SaveChangesAsync(cancellationToken);

                return ReturnedResponse.SuccessResponse(null, "Succesfully Updated Entry");
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(UpdateYulcomCommand)} Service ", errMessage));
                return ReturnedResponse.ErrorResponse($"Any Error Occured Processing Request", null);
            }
        }

        public async Task<ApiResponse> Handle(DeleteYulcomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var yulcom = await yulcomContext.YulcomAssesmentData.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (yulcom is null) return ReturnedResponse.ErrorResponse("Id does not exist", null);

                yulcomContext.YulcomAssesmentData.Remove(yulcom);

                await yulcomContext.SaveChangesAsync(cancellationToken);

                return ReturnedResponse.SuccessResponse(null, "Succesfully Deleted Entry");
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(DeleteYulcomCommand)} Service ", errMessage));
                return ReturnedResponse.ErrorResponse($"Any Error Occured Processing Request", null);
            }
        }

        public async Task<ApiResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var yulcom = await yulcomContext.YulcomAssesmentData.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (yulcom is null) return ReturnedResponse.ErrorResponse("Id does not exist", null);

                return ReturnedResponse.SuccessResponse(null, yulcom.AsDto());
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(GetByIdQuery)} Service ", errMessage));
                return ReturnedResponse.ErrorResponse($"Any Error Occured Processing Request", null);
            }
        }

        public async Task<ApiResponse> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                 var response =  await yulcomContext.YulcomAssesmentData
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                return ReturnedResponse.SuccessResponse(null, response.AsDto());
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(GetAllQuery)} Service ", errMessage));
                return ReturnedResponse.ErrorResponse($"Any Error Occured Processing Request", null);
            }

        }
    }

    


}
