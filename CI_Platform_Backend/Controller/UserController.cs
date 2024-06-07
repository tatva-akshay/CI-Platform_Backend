using AutoMapper;
using CI_Platform_Backend_Presentation.DTO.User;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Mvc;


namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get User Data using User ID
    // This Data will be as per Edit user page's required Data
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAsync(long id)
    {
        UserDTO user = await _userService.GetAsync(id);
        return (user == null || user.UserId == 0) ? 
            NotFound() :
            Ok(user);
    }


    [HttpPut]
    [Route("")]
    public async Task<ActionResult> UpdateAsync(long id, UpdateUserDTO updateUserDTO)
    {
        if(await _userService.IsExistAsync(id))
        {
            return await _userService.UpdateAsync(id, updateUserDTO) ? 
                Ok() : 
                BadRequest();
        }
        return NotFound();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Change Password of User Account
    // It's for Logged In Users Only
    [HttpPost]
    [Route("change-password")]
    public async Task<ActionResult> ChangePasswordAsync(long id, ChangePasswordDTO changePasswordDTO)
    {
        if(!await _userService.IsExistAsync(id))
        {
            return NotFound();
        }
        if(!await _userService.IsValidAsync(id, changePasswordDTO.OldPassword))
        {
            return BadRequest();
        }
        return await _userService.ChangePasswordAsync(id, changePasswordDTO.NewPassword) ?
            Ok() : 
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Change User Skills
    // It requires User ID and List of Skill IDs
    [HttpPost]
    [Route("change-skills")]
    public async Task<ActionResult> ChangeSkillsAsync(long id, List<long> skillIds)
    {
        if(!await _userService.IsExistAsync(id))
        {
            return NotFound();
        }        
        return await _userService.ChangeSkillsAsync(id, skillIds) ?
            Ok() : 
            BadRequest();
    }

    [HttpPost]
    [Route("profile-image")]
    public async Task<ActionResult> Image(long id, IFormFile image)
    {
        if(await _userService.IsExistAsync(id))
        {
            byte[] imageBytes;
            using (var item = new MemoryStream())
            {
                image.CopyTo(item);
                imageBytes = item.ToArray();
            }
            return await _userService.UpdateImageAsync(id, imageBytes) ?
                Ok(imageBytes) : 
                BadRequest();
        }
        return NotFound();
    }

    [HttpGet]
    [Route("profile-image")]
    public async Task<ActionResult> Download(long id)
    {
        if(await _userService.IsExistAsync(id))
        {
            UserDTO user = await _userService.GetAsync(id);
            return File(user.ProfileImage, "application/octet-stream", "abc.png");
        }
        return NotFound();
    }
}
