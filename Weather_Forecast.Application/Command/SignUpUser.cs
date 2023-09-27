using MediatR;
using Weather_Forecast.Application.Helpers;
using Weather_Forecast.Domain.Models;
using Weather_Forecast.Repository.Abstractions.Users;

namespace Weather_Forecast.Application.Command
{
    public class SignUpUser
    {
        public record SignUpRequest(string email, string password, string firstName, string lastName) : IRequest<SignUpResult>;

        public record SignUpResult(bool logedIn);

        public class Handler : IRequestHandler<SignUpRequest, SignUpResult>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<SignUpResult> Handle(SignUpRequest request, CancellationToken cancellationToken)
            {
                //We can do validations on the backend also, not only if the data is there but if it is valid also.
                //We can do this in the domain also by writeing methods that check the validity.
                //We can use Fluent validator library also.
                if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password) || string.IsNullOrEmpty(request.firstName) || string.IsNullOrEmpty(request.lastName))
                    throw new Exception("Invalid User");

                var res = _userRepository.Add(new User(request.firstName,request.lastName, new EncryptHelper().MD5Encrypt(request.email), new EncryptHelper().MD5Encrypt(request.password)));

                return new SignUpResult(true);
            }
        }
    }
}
