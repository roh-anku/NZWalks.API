using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        //api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Username
            };
            var response = await _userManager.CreateAsync(identityUser, registerUserDto.Password);

            if (response.Succeeded)
            {
                response = await _userManager.AddToRolesAsync(identityUser, registerUserDto.Roles);

                if (response.Succeeded)
                    return Ok("Successfully user registered, please login");
            }

            return BadRequest("something went wrong");
        }

        //post: api/auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
           var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
               var isValidPassword = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

                if (isValidPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //create token
                       var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        LoginResponseDto loginResponse = new();
                        loginResponse.JwtToken = jwtToken;
                        return Ok(loginResponse);
                    }
                }
            }
            return BadRequest("invalid username or password");
        }
    }
}
