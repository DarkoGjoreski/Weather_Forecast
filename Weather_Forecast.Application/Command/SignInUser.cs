using MediatR;
using Weather_Forecast.Application.Helpers;
using Weather_Forecast.Domain.Models;
using Weather_Forecast.Repository.Abstractions.Users;

namespace Weather_Forecast.Application.Command
{
    public class SignInUser
    {
        public record SignInRequest(string email, string password) : IRequest<SignInResult>;

        public record SignInResult(User user);

        public class Handler : IRequestHandler<SignInRequest, SignInResult>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<SignInResult> Handle(SignInRequest request, CancellationToken cancellationToken)
            {
                //We can do validations on the backend also, not only if the data is there but if it is valid also.
                //We can do this in the domain also by writeing methods that check the validity.
                //We can use Fluent validator library also.
                if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
                    throw new Exception("Invalid User");

                var encEmail = new EncryptHelper().MD5Encrypt(request.email);
                var encPass = new EncryptHelper().MD5Encrypt(request.password);

                var user = _userRepository.GetUserByUserNameAndPassword(new User()
                {
                    UserName = encEmail,
                    Password = encPass,
                });

                return new SignInResult(user);
            }
        }
    }
}
