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
                new Claim[]
                {
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
                new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }
            ),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!)), SecurityAlgorithms.HmacSha256Signature)
        }));

    }

    public bool ValidateResetPasswordToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"] ?? "");

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            if ((JwtSecurityToken)validatedToken != null)
            {
                return true;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return false;
    }
    
    public string GetEmailFromToken(string token)
    {               
        return new JwtSecurityTokenHandler().
                ReadJwtToken(token).
                Claims.
                FirstOrDefault(c => c.Type == "email")?.Value ?? "";                
    }

}
