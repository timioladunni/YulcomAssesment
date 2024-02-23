using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecurityLibrary.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YulcomAssesment.API.Configurations;
using YulcomAssesment.API.Controllers;
using YulcomAssesment.API.Data;
using YulcomAssesment.API.Models.Application;
using YulcomAssesment.API.Models.Response;
using YulcomAssesment.API.Response;
using YulcomAssesment.API.Services.Interface;

namespace YulcomAssesment.API.Services.Implementation
{
    public class ApplicationService : IApplicationService
    {
        private readonly YulcomAssesmentConfiguration configuration;
        private readonly YulcomAssesmentContext yulcomContext;
        private readonly ILogger<ApplicationService> logger;


        public ApplicationService(YulcomAssesmentConfiguration configuration, 
            YulcomAssesmentContext yulcomContext, ILogger<ApplicationService> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.yulcomContext = yulcomContext ?? throw new ArgumentNullException(nameof (yulcomContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof (logger));
        }

        public async Task<ApiResponse> GenerateTokenAsync(GenerateTokenRequestDto model)
        {
            try
            {
                var apiKey = await yulcomContext.ApiKeys.FirstOrDefaultAsync(x=> 
                    x.PublicApiKey == EncryptionUtility.Encrypt(model.ClientId) 
                    && x.SecretApiKey == EncryptionUtility.Encrypt(model.ClientSecret));

                if (apiKey is null) return ReturnedResponse.ErrorResponse("API credentials are incorrect", null);

                var responseDto = await GenerateJwtTokenAsync(apiKey);

                return ReturnedResponse.SuccessResponse(null, responseDto);
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message == null ? ex.InnerException.ToString() : ex.Message;
                logger.LogError(string.Concat($"Error occured in the {nameof(GenerateTokenAsync)} Service ", errMessage));
                return ReturnedResponse.ErrorResponse($"Any Error Occured Processing Request", null);
            }
        }

        private async Task<GenerateTokenResponseDto> GenerateJwtTokenAsync(ApiKey model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(EncryptionUtility.Decrypt(configuration.EncryptionKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, model.Name)
                }),
                Expires = DateTime.Now.AddMinutes(configuration.JwtLifespan),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            GenerateTokenResponseDto responseDto = new()
            {
                AccessToken = tokenHandler.WriteToken(token),
                TokenType = "Bearer",
                ExpiresIn = tokenDescriptor.Expires.Value,
                RefreshToken = EncryptionUtility.GenerateRefereshToken(),
            };

            return responseDto;
        }
    }
}
