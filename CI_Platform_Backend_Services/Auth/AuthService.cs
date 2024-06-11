using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Carousel;
using CI_Platform_Backend_Presentation.DTO.Login;
using CI_Platform_Backend_Repository.LoginCarousel;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly ILoginCarouselRepo _loginCarouselRepo;

    public AuthService(IUserRepo userRepo, ILoginCarouselRepo loginCarouselRepo)
    {
        _userRepo = userRepo;
        _loginCarouselRepo = loginCarouselRepo;
    }

    public async Task<string> IsValidUserAsync(LoginDTO loginDTO)
    {
        CI_Platform_Backend_DBEntity.DbModels.User user = await _userRepo.GetAsync(x => x.Email == loginDTO.Email);

        return !(user == null ||  user.UserId == 0) && user.Password == loginDTO.Password ? user.FirstName + " " + user.LastName : "";
    }

    public async Task<bool> IsUserExistAsync(string email)
    {
        CI_Platform_Backend_DBEntity.DbModels.User user = await _userRepo.GetAsync(x => x.Email == email);

        return !(user == null || user.UserId == 0);
    }

    public async Task<bool> ResetPasswordAsync(string email, string password)
    {
        CI_Platform_Backend_DBEntity.DbModels.User user = await _userRepo.GetAsync(x => x.Email == email);
     
        if(user == null || user.UserId == 0)
        {
            return false;
        }
        user.Password = password;
        return await _userRepo.UpdateAsync(user);        
    }

    public async Task<bool> RegisterUserAsync(CI_Platform_Backend_DBEntity.DbModels.User user)
    {
        return await _userRepo.AddAsync(user);
    }

    public async Task<List<LoginCarousel>> GetCarouselsAsync()
    {
        return await _loginCarouselRepo.GetAsync();
    }

    public async Task<bool> AddCarouselAsync(LoginCarousel loginCarousel)
    {
        return await _loginCarouselRepo.AddAsync(new LoginCarousel()
        {
            CarouselHead = loginCarousel.CarouselHead,
            CarouselText = loginCarousel.CarouselText,
            CarouselImage = loginCarousel.CarouselImage,
        });
    }


}
