using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost(Name ="login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.UserName == "admin" && login.Password == "admin") 
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Employee_portal_secret_key_here."));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "EmployeeManagementPortal",
                audience: "EmployeeManagementPortal",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
