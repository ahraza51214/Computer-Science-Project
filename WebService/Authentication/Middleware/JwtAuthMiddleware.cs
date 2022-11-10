using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ProjectPortfolio2_Group11.Authentication.Middleware
{
    public static class JwtAuthMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthMiddleware>();
        }
    }

    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DataServiceFacade _dataServiceFacade;
        private readonly IConfiguration _configuration;

        public JwtAuthMiddleware(RequestDelegate next, DataServiceFacade dataServiceFacade, IConfiguration configuration)
        {
            _next = next;
            _dataServiceFacade = dataServiceFacade;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Auth:Secret").Value);

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "id");
                if (claim != null)
                {
                    int.TryParse(claim.Value.ToString(), out var id);
                    context.Items["User"] = _dataServiceFacade.UsersDs.AuthenticationGetUser(id);
                }
            }
            catch { }

            await _next(context);
        }
    }
}