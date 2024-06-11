using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Carousel;
using CI_Platform_Backend_Presentation.DTO.Login;

namespace CI_Platform_Backend_Services.Auth;

public interface IAuthService
{
    Task<string> IsValidUserAsync(LoginDTO loginDTO);

    Task<bool> IsUserExistAsync(string email);

    Task<bool> ResetPasswordAsync(string email, string password);

    Task<bool> RegisterUserAsync(CI_Platform_Backend_DBEntity.DbModels.User user);

    Task<List<LoginCarousel>> GetCarouselsAsync();

    Task<bool> AddCarouselAsync(LoginCarousel loginCarousel);

}
