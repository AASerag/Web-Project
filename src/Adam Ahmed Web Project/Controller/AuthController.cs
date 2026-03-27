using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Adam_Ahmed_Web_Project.Dtos;
using Hangfire; // Added for the bonus!

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) => _config = config;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (login.Username == "admin" && login.Password == "password123")
            {
                var token = GenerateJwtToken(login.Username, "Admin");
                var refreshToken = Guid.NewGuid().ToString(); // Bonus: Generate a refresh token

                return Ok(new AuthResponseDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Username = login.Username
                });
            }
            else if (login.Username == "student" && login.Password == "student123")
            {
                var token = GenerateJwtToken(login.Username, "User");
                var refreshToken = Guid.NewGuid().ToString();

                return Ok(new AuthResponseDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Username = login.Username
                });
            }

            return Unauthorized("Invalid credentials");
        }

      
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] string refreshToken)
        {
           
            if (string.IsNullOrEmpty(refreshToken)) return BadRequest("Invalid Refresh Token");

            // If valid, give them a fresh JWT
            var newToken = GenerateJwtToken("admin", "Admin");
            return Ok(new { Token = newToken });
        }

      
        [HttpPost("trigger-sync")]
        public IActionResult TriggerSync()
        {
            // This schedules a background job immediately!
            BackgroundJob.Enqueue(() => Console.WriteLine("Manual sync started by Admin via Hangfire!"));
            return Ok("Background job has been queued.");
        }

        private string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}