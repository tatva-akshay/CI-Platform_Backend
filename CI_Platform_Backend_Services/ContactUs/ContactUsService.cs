using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Repository.ContactUs;

namespace CI_Platform_Backend_Services.ContactUs;

public class ContactUsService : IContactUsService
{

    private readonly IContactUsRepo _contactUsRepo;

    public ContactUsService(IContactUsRepo contactUsRepo)
    {
        _contactUsRepo = contactUsRepo;
    }

    public async Task<bool> AddAsync(long userId, ContactUss contactUs)
    {
        contactUs.UserId = userId;
        return await _contactUsRepo.AddAsync(contactUs);
    }

}
