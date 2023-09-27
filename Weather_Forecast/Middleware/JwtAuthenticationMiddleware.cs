using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Weather_Forecast.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IOptionsMonitor<JwtBearerOptions> jwtOptions)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var parameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;

                    var claimsPrincipal = tokenHandler.ValidateToken(token, parameters, out var validatedToken);
                    context.User = claimsPrincipal;
                }
                catch (Exception)
                {
                    // Token validation failed
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }
            }

            await _next(context);
        }
    }
}