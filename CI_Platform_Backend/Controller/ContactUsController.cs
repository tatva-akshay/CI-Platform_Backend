using AutoMapper;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.ContactUs;
using CI_Platform_Backend_Services.ContactUs;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("contact-us")]
public class ContactUsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IContactUsService _contactUsService;
    private readonly IMapper _mapper;

    public ContactUsController(IUserService userService, IMapper mapper, IContactUsService contactUsService)
    {
        _userService = userService;
        _mapper = mapper;
        _contactUsService = contactUsService;
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Add Contact Us Query
    [HttpPost]
    [Route("")]
    public async Task<ActionResult> AddAsync(long id, ContactUsDTO contactUsDTO)
    {
        if(!await _userService.IsExistAsync(id))
        {
            return NotFound();
        }
        return await _contactUsService.AddAsync(id, _mapper.Map<ContactUss>(contactUsDTO)) ?
            Ok() :
            BadRequest();
    }
}
