using CI_Platform_Backend_Presentation.DTO.Login;

namespace CI_Platform_Backend_Services.Login;

public interface ILoginService
{
    Task<bool> IsValidUserAsync(LoginDTO loginDTO);

    Task<bool> IsUserExistAsync(string email);

    Task<bool> ResetPasswordAsync(string email, string password);


}
