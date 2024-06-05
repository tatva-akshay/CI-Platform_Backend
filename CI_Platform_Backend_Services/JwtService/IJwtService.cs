namespace CI_Platform_Backend_Services;

public interface IJwtService
{
    string AuthenticationToken(string email);

    string ResetPasswordToken(string email);

    bool ValidateResetPasswordToken(string token);

    string GetEmailFromToken(string token);
}
