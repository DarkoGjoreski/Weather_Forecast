using Microsoft.EntityFrameworkCore;
using Weather_Forecast.Domain.Models;

namespace Weather_Forecast.Repository
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
