using Weather_Forecast.Domain.Models;
using Weather_Forecast.Repository;
using Weather_Forecast.Repository.Abstractions.Users;

namespace Weather_Forcast.Repository.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        protected WeatherDbContext _context { get; set; }
        public UserRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public User GetUserByUserNameAndPassword(User user)
        {
            if (user != null)
                return _context.Set<User>().FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            return null;
        }

        public int Add(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user.Id;
        }
    }
}
