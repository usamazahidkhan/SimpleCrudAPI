using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperSoft.WebAPI.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthenticateController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequest request)
        {
            if (ValidateCredientials(request))
            {
                byte[] key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name, request.userName),
                    }),
                    Expires = DateTime.UtcNow.AddSeconds(200),
                    Issuer = configuration["Jwt:Issuer"],
                    Audience = configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(
                                            new SymmetricSecurityKey(key),
                                            SecurityAlgorithms.HmacSha512Signature
                                         )
                };

                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }

            return Unauthorized();
        }

        private static bool ValidateCredientials(LoginRequest login)
            => login?.userName == "admin" && login?.password == "password";
    }
}