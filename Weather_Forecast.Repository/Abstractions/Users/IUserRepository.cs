using Weather_Forecast.Domain.Models;

namespace Weather_Forecast.Repository.Abstractions.Users
{
    public interface IUserRepository
    {
        User GetUserByUserNameAndPassword(User user);
        int Add(User user);
    }
}
