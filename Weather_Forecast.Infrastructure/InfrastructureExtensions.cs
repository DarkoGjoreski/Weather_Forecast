using Microsoft.Extensions.DependencyInjection;
using Weather_Forecast.Application.Abstracitions;
using Weather_Forecast.Infrastructure.Services;

namespace Weather_Forecast.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureExtensions(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IWeatherService, WeatherService>();
            return services;
        }
    }
}
