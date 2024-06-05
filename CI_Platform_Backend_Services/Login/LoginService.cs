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
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.Email == loginDTO.Email);
     
        if(user == null || user.UserId == 0)
        {
            return false;
        }
        return user.Password == loginDTO.Password;
    }

    public async Task<bool> IsUserExistAsync(string email)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.Email == email);
        return !(user == null || user.UserId == 0);
    }

    public async Task<bool> ResetPasswordAsync(string email, string password)
    {

        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.Email == email);
     
        if(user == null || user.UserId == 0)
        {
            return false;
        }

        user.Password = password;

        if(await _userRepo.UpdateAsync(user))
        {
            return true;
        }

        return false;
    }
}
