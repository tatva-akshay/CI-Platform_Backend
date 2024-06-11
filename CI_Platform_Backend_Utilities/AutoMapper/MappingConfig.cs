using AutoMapper;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation.DTO.Register;
using CI_Platform_Backend_Presentation.DTO.CreateTheme;
using CI_Platform_Backend_Presentation.DTO.Theme;
using CI_Platform_Backend_Presentation.DTO.CreateSkill;
using CI_Platform_Backend_Presentation.DTO.Skill;
using CI_Platform_Backend_Presentation.DTO.CMSPages;
using CI_Platform_Backend_Presentation.DTO.User;
using CI_Platform_Backend_Presentation.DTO.ContactUs;
using CI_Platform_Backend_Presentation.DTO.Carousel;

namespace CI_Platform_Backend_Utilities.AutoMapper;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, RegisterDTO>().ReverseMap();

        CreateMap<Theme, CreateThemeDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Theme1))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
        .ReverseMap()
            .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
            .ForMember(src => src.Theme1, opt => opt.MapFrom(dest => dest.Name));

        CreateMap<Theme, ThemeDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Theme1))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ThemeID, opt => opt.MapFrom(src => src.ThemeId))
        .ReverseMap()
            .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
            .ForMember(src => src.Theme1, opt => opt.MapFrom(dest => dest.Name))
            .ForMember(src => src.ThemeId, opt => opt.MapFrom(dest => dest.ThemeID));

        CreateMap<Skill, CreateSkillDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Skills))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
        .ReverseMap()
            .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
            .ForMember(src => src.Skills, opt => opt.MapFrom(dest => dest.Name));

        CreateMap<Skill, SkillDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Skills))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.SkillID, opt => opt.MapFrom(src => src.SkillId))
        .ReverseMap()
            .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
            .ForMember(src => src.Skills, opt => opt.MapFrom(dest => dest.Name))
            .ForMember(src => src.SkillId, opt => opt.MapFrom(dest => dest.SkillID));

        CreateMap<CmsPrivacyPolicy, CreateCMSPageDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PageTitle))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.PageDescription))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
        .ReverseMap()
            .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
            .ForMember(src => src.PageTitle, opt => opt.MapFrom(dest => dest.Title))
            .ForMember(src => src.PageDescription, opt => opt.MapFrom(dest => dest.Description))
            .ForMember(src => src.Slug, opt => opt.MapFrom(dest => dest.Slug));

        CreateMap<CmsPrivacyPolicy, CMSPageDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PageTitle))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.PageDescription))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
            .ForMember(dest => dest.CMSPageID, opt => opt.MapFrom(src => src.CmsId))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
        .ReverseMap()
            .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
            .ForMember(src => src.PageTitle, opt => opt.MapFrom(dest => dest.Title))
            .ForMember(src => src.PageDescription, opt => opt.MapFrom(dest => dest.Description))
            .ForMember(src => src.CmsId, opt => opt.MapFrom(dest => dest.CMSPageID))
            .ForMember(src => src.Slug, opt => opt.MapFrom(dest => dest.Slug));

        CreateMap<LoginCarousel, CarouselDTO>()
            .ForMember(dest => dest.CarouselId, opt => opt.MapFrom(src => src.CarouselId))
            .ForMember(dest => dest.Head, opt => opt.MapFrom(src => src.CarouselHead))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.CarouselImage))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CarouselText));
        CreateMap<CreateCarouselDTO, LoginCarousel>()
            .ForMember(dest => dest.CarouselHead, opt => opt.MapFrom(src => src.Header))
            .ForMember(dest => dest.CarouselText, opt => opt.MapFrom(src => src.Description));

        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<ContactUss, ContactUsDTO>().ReverseMap();
        // CreateMap<Skill, SkillDTO>()
        //     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Skills))
        //     .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Status))
        //     .ForMember(dest => dest.SkillID, opt => opt.MapFrom(src => src.SkillId))
        // .ReverseMap()
        //     .ForMember(src => src.Status, opt => opt.MapFrom(dest => dest.IsActive))
        //     .ForMember(src => src.Skills, opt => opt.MapFrom(dest => dest.Name))
        //     .ForMember(src => src.SkillId, opt => opt.MapFrom(dest => dest.SkillID));
    }
}
