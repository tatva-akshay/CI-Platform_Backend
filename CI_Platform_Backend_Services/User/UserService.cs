using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.User;
using CI_Platform_Backend_Repository.Skill;
using CI_Platform_Backend_Repository.User;
using CI_Platform_Backend_Repository.UserRepo;

namespace CI_Platform_Backend_Services.User;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;

    private readonly IUserInformationRepo _userInformationRepo;

    private readonly ISkillRepo _skillRepo;

    public UserService(IUserRepo userRepo, ISkillRepo skillRepo, IUserInformationRepo userInformationRepo)
    {
        _userRepo = userRepo;
        _skillRepo = skillRepo;
        _userInformationRepo = userInformationRepo;
    }

    public async Task<UserDTO> GetAsync(long id)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        if(user == null || user.UserId == 0)
        {
            return new UserDTO();
        }
        UserInformation userInformation = await _userInformationRepo.GetAsync(x => x.UserId == id);
        if(userInformation == null && userInformation.InformationId == 0)
        {
            return new UserDTO();
        }
        UserDTO userDTO = new UserDTO()
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            EmployeeId = user.EmployeeId,
            Department = user.Department,
            CityId = user.CityId.Value,
            CountryId = user.CountryId.Value,
            Summary = user.ProfileText,
            Description = userInformation.Description,
            Availability = userInformation.Availability,
            Gender = userInformation.Gender,
            AgeGroup = userInformation.AgeGroup,
            Title = user.Title,
            WhyIVolunteer = user.WhyIVolunteer,
            Skills = user.Skills
        };
        
        return userDTO;
    }

    public async Task<bool> IsExistAsync(long id)
    {
        return await _userRepo.IsExistAsync(id);
    }

    public async Task<bool> IsValidAsync(long id, string oldPassword)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        if(user != null && user.UserId > 0 && user.Password == oldPassword)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> ChangePasswordAsync(long id, string password)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        if(user != null && user.UserId > 0)
        {
            user.Password = password;
            return await _userRepo.UpdateAsync(user);
        }
        return false;
    }

    public async Task<bool> ChangeSkillsAsync(long id, List<long> skillIds)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        if(user != null && user.UserId > 0)
        {
            user.Skills = String.Join(", ", await _skillRepo.GetAsync(skillIds));
            return await _userRepo.UpdateAsync(user);
        }
        return false;
    }

}
