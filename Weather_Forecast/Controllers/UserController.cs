using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather_Forecast.Application.Abstracitions;
using static Weather_Forecast.Application.Command.SignInUser;
using static Weather_Forecast.Application.Command.SignUpUser;

namespace Weather_Forecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITokenService _tokenService;

        public UserController(ILogger<WeatherForecastController> logger, IMediator mediator, ITokenService tokenService)
        {
            _logger = logger;
            _mediator = mediator;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<string>> SignIn([FromBody] SignInRequest userSignIn)
        {
            var signInUserResponse = await _mediator.Send(userSignIn);

            if (signInUserResponse is null || signInUserResponse.user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _tokenService.GenerateJwtToken(signInUserResponse.user);
            return Ok(token);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest userSignUp)
        {
            var result = await _mediator.Send(userSignUp);
            return Ok(result);
        }
    }
}
