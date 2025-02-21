using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaymentFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("token")]
       public IActionResult GenerateToken()
       {
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key"));
           var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           var claims = new[]
           {
               new Claim(JwtRegisteredClaimNames.Sub, "user"),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
           };
           var token = new JwtSecurityToken(
               issuer: "issuer",
               audience: "audience",
               claims: claims,
               expires: DateTime.UtcNow.AddHours(1),
               signingCredentials: credentials
           );
           return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
       }
    }
}
