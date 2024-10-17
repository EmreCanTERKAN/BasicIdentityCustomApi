using BasicIdentityCustomApi.Dtos;
using BasicIdentityCustomApi.Services;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = BasicIdentityCustomApi.Models.LoginRequest;
using RegisterRequest = BasicIdentityCustomApi.Models.RegisterRequest;

namespace BasicIdentityCustomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user =  new AddUserDto()
            {
                Email = request.Email,
                Password = request.Password
            };

           var result = await _userService.AddUser(user);

            if (result.IsSucceed)
                return Ok(result);
            else
                return BadRequest(result.Message);

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginUserDto = new LoginUserDto()
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password
            };

            var result = await _userService.LoginUser(loginUserDto)
        }
    }
}
