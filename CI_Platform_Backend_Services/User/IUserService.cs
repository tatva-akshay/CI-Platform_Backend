using CI_Platform_Backend_Presentation.DTO.User;

namespace CI_Platform_Backend_Services.User;

public interface IUserService
{
    Task<UserDTO> GetAsync(long id);

    Task<bool> IsExistAsync(long id);

    Task<bool> IsValidAsync(long id, string oldPassword);

    Task<bool> ChangePasswordAsync(long id, string password);

    Task<bool> ChangeSkillsAsync(long id, List<long> skillIds);

}
