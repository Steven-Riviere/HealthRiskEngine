using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAPI.Models;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // ----------------- REGISTER -----------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null) return BadRequest("Username already taken");

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully");
        }

        // ----------------- LOGIN -----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid credentials");

            var issuer = _configuration["Authentication:Schemes:Bearer:ValidIssuer"];
            var audience = _configuration["Authentication:Schemes:Bearer:ValidAudience"];
            var secret = _configuration["Authentication:Schemes:Bearer:Secret"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secret))
                throw new InvalidOperationException("JWT configuration is missing.");

            var key = Encoding.UTF8.GetBytes(secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}