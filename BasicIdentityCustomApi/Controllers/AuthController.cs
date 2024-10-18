using BasicIdentityCustomApi.Dtos;
using BasicIdentityCustomApi.Jwt;
using BasicIdentityCustomApi.Models;
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
        [HttpPost("register")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginUserDto = new LoginUserDto()
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password
            };
            var result = await _userService.LoginUser(loginUserDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            var user = result.Data;

            // yalnızca buradan çekmek istediğimiz için properti ile çektik 
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinute"]!)

            });


            return Ok(new LoginResponse
            {
                Message = "Giriş Başarıyla tamamlandı",
                Token = token
            });

            

        }

       

    }
}
