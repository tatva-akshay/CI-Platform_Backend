using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.Login;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.Login;

public class LoginService : ILoginService
{
    private readonly IUserRepo _userRepo;

    public LoginService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> IsValidUserAsync(LoginDTO loginDTO)
    {
        User user = await _userRepo.GetByEmailAsync(loginDTO.Email);
     
        if(user == null || user.UserId == 0)
        {
            return false;
        }
        return user.Password == loginDTO.Password;
    }

    public async Task<bool> IsUserExistAsync(string email)
    {
        User user = await _userRepo.GetByEmailAsync(email);
        return !(user == null || user.UserId == 0);
    }
}
