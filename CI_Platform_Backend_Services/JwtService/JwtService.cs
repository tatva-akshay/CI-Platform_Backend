using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CI_Platform_Backend_Services.JwtService;

public class JwtService : IJwtService
{

    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string AuthenticationToken(string email)
    {
        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(
                new Claim[]{
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role,"Other")
                }
            ),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!)), SecurityAlgorithms.HmacSha256Signature)
        }));

    }

    public string ResetPasswordToken(string email)
    {
        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(
                new Claim[]{
                    new Claim(ClaimTypes.Email, email)
                }
            ),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!)), SecurityAlgorithms.HmacSha256Signature)
        }));

    }

}
