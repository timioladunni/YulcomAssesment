using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YulcomAssesment.API.Models.Response;
using YulcomAssesment.API.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace YulcomAssesment.API.Extensions.ActionFilters
{
    public class AuditLogFilter : ActionFilterAttribute
    {
        private readonly YulcomAssesmentContext dbContext;
        public AuditLogFilter(YulcomAssesmentContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task OnResourceExecuted(ResourceExecutedContext context)
        {
            string? ip;
            try
            {
                ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                ip = string.Empty;
            }

            var name = BaseFilter.GetNameClaim(context);
            var url = context.HttpContext.Request.Path.ToUriComponent();

            AuditTrail auditTrail = new()
            {
                DateAdded = DateTime.UtcNow,
                ApiUser = name,
                EndpointCalled = url,
                IpAddress = ip,
            };

            await dbContext.AuditTrails.AddAsync(auditTrail);
            await dbContext.SaveChangesAsync();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {

        }
    }
}
