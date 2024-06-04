using AutoMapper;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.Register;

namespace CI_Platform_Backend_Utilities.AutoMapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, RegisterDTO>().ReverseMap();
    }
}
