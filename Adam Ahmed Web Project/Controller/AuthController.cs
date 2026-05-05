using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Adam_Ahmed_Web_Project.Dtos;
using Adam_Ahmed_Web_Project.DataBase;
using Adam_Ahmed_Web_Project.Models;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.AspNetCore.Authorization; // Required for [Authorize]

namespace Adam_Ahmed_Web_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest("Username is already taken.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Role = request.Role ?? "User" 
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            // Generate the Token
            var token = GenerateJwtToken(user.Username, user.Role);

            // Set the token inside a secure HttpOnly Cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, 
                Secure = false, // Must be false for local HTTP testing
                SameSite = SameSiteMode.Lax, 
                Expires = DateTime.UtcNow.AddHours(2)
            };

            Response.Cookies.Append("jwtToken", token, cookieOptions);

            return Ok(new 
            { 
                Message = "Login successful", 
                Username = user.Username, 
                Role = user.Role 
            });
        }

        [Authorize(Roles = "Admin")] 
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new { u.Id, u.Username, u.Role }) 
                .ToListAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            // Prevent an Admin from accidentally deleting themselves
            var currentUsername = User.Identity?.Name;
            if (user.Username == currentUsername) return BadRequest("You cannot delete your own account.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("User deleted successfully.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax
            });

            return Ok(new { Message = "Logged out successfully" });
        }

        [Authorize(Roles = "Admin")] // Locked to Admin only
        [HttpPost("trigger-sync")]
        public IActionResult TriggerSync()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Manual sync started by Admin via Hangfire!"));
            return Ok("Background job has been queued.");
        }

        private string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role), // This matches your [Authorize(Roles = "Admin")]
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}