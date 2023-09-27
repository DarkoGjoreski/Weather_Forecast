using Weather_Forecast.Domain.Models;

namespace Weather_Forecast.Application.Abstracitions
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
