using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Weather_Forecast.Application.Abstracitions;
using Weather_Forecast.Domain.Models;

namespace Weather_Forecast.Infrastructure.Services
{
    public class TokenService : ITokenService
    {

        public string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes("48d9e5e6-347a-4633-adad-e7043a4d40fe"); // Replace with your secret key
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
