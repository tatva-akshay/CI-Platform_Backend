using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.Register;

public class RegisterUserService : IRegisterUserService
{
    private readonly IUserRepo _userRepo;

    public RegisterUserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<bool> RegisterUserAsync(User user)
    {
        return await _userRepo.AddAsync(user);
    }
}
