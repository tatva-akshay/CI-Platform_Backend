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
        
        if(userInformation == null && userInformation?.InformationId == 0)
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
            CityId = user.CityId,
            CountryId = user.CountryId,
            Summary = user.ProfileText,
            Description = userInformation?.Description ?? "",
            Availability = userInformation?.Availability ?? 1,
            Gender = userInformation?.Gender ?? 1,
            AgeGroup = userInformation?.AgeGroup ?? 1,
            Title = user.Title,
            WhyIVolunteer = user.WhyIVolunteer,
            Skills = user.Skills,
            ProfileImage = user.Avatar!           
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
        
        return user != null && user.UserId > 0 && user.Password == oldPassword;
    }

    public async Task<bool> ChangePasswordAsync(long id, string password)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        
        if(user != null && user.UserId > 0)
        {
            user.Password = password;
            user.UpdatedAt = DateTime.Now;
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
            user.UpdatedAt = DateTime.Now;
            return await _userRepo.UpdateAsync(user);
        }
        return false;
    }

    public async Task<bool> UpdateAsync(long id, UpdateUserDTO updateUserDTO)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        
        if(user != null && user.UserId > 0)
        {
            user.FirstName = updateUserDTO.FirstName;
            user.LastName = updateUserDTO.LastName;
            user.Email = updateUserDTO.Email;
            user.WhyIVolunteer = updateUserDTO.WhyIVolunteer;
            user.EmployeeId = updateUserDTO.EmployeeId;
            user.Department = updateUserDTO.Department;
            user.CityId = updateUserDTO.CityId;
            user.CountryId = updateUserDTO.CountryId;
            user.ProfileText = updateUserDTO.ProfileSummary;
            user.Title = updateUserDTO.Title;
            user.UpdatedAt = DateTime.Now;
            await _userRepo.UpdateAsync(user);

            UserInformation userInformation = await _userInformationRepo.GetAsync(x => x.UserId == id);
            if(userInformation == null || userInformation.InformationId == 0)
            {
                await _userInformationRepo.AddAsync( new UserInformation()
                    {
                        UserId = id,
                        Description = updateUserDTO.Description,
                        Gender = updateUserDTO.Gender,
                        Availability = updateUserDTO.Availability,
                        AgeGroup = updateUserDTO.AgeGroup,
                    }
                );
            }
            else
            {
                userInformation.Description = updateUserDTO.Description;
                userInformation.Gender = updateUserDTO.Gender;
                userInformation.Availability = updateUserDTO.Availability;
                userInformation.AgeGroup = updateUserDTO.AgeGroup;
                await _userInformationRepo.UpdateAsync(userInformation);
            }
            return true;
        }
        return false;
    }

    public async Task<bool> UpdateImageAsync(long id, byte[] image)
    {
        CI_Platform_Backend_DBEntity.DataModels.User user = await _userRepo.GetAsync(x => x.UserId == id);
        
        if(user != null && user.UserId > 0)
        {
            user.Avatar = image;
            user.UpdatedAt = DateTime.Now;
            return await _userRepo.UpdateAsync(user);
        }
        return false;
    }

}
