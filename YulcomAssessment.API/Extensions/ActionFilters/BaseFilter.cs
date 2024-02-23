using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace YulcomAssesment.API.Extensions.ActionFilters
{
    public static class BaseFilter
    {
        public static string GetNameClaim(ResourceExecutedContext context)
        {
            var result = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;

            if (result != null)
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    if (handler.CanReadToken(token))
                    {
                        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                        // Access claims from the JWT
                        var name = jsonToken?.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                        return name;
                    }
                }
            }

            return null;
        }
    }
}
