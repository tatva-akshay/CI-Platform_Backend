
namespace CI_Platform_Backend_Services.Email;

public interface IEmailService
{    
    Task<bool> SendResetPasswordAsync(string email, string token);

}
