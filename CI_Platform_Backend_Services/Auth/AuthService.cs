using CI_Platform_Backend_Presentation.DTO.Login;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    public AuthService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> IsValidUserAsync(LoginDTO loginDTO)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.Email == loginDTO.Email);

        return !(user == null ||  user.UserId == 0) && user.Password == loginDTO.Password;
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
        return await _userRepo.UpdateAsync(user);        
    }

    public async Task<bool> RegisterUserAsync(CI_Platform_Backend_DBEntity.DataModels.User user)
    {
        return await _userRepo.AddAsync(user);
    }
}
