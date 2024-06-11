using CI_Platform_Backend_DBEntity.DbModels;

namespace CI_Platform_Backend_Services.ContactUs;

public interface IContactUsService
{

    Task<bool> AddAsync(long userId, ContactUss contactUs);
}
