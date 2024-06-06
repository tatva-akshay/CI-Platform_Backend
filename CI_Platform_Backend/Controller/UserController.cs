using AutoMapper;
using CI_Platform_Backend_Presentation.DTO.User;
using CI_Platform_Backend_Services.User;
using Microsoft.AspNetCore.Mvc;


namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("api/[controller]")]
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
    [Route("")]
    public async Task<ActionResult> GetAsync(long id)
    {
        try
        {
            UserDTO user = await _userService.GetAsync(id);
            if(user == null || user.UserId == 0)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }


    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(long id, UpdateUserDTO updateUserDTO)
    {
        try
        {
            if(await _userService.IsExistAsync(id))
            {
                if(await _userService.UpdateAsync(id, updateUserDTO))
                {
                    return Ok();
                }
                return BadRequest();
            }
           return NotFound();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Change Password of User Account
    // It's for Logged In Users Only
    [HttpPost]
    [Route("change-password")]
    public async Task<ActionResult> ChangePasswordAsync(long id, ChangePasswordDTO changePasswordDTO)
    {
        try
        {
            if(!await _userService.IsExistAsync(id))
            {
                return NotFound();
            }
            if(!await _userService.IsValidAsync(id, changePasswordDTO.OldPassword))
            {
                return BadRequest();
            }
            if(await _userService.ChangePasswordAsync(id, changePasswordDTO.NewPassword))
            {
                return Ok();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Change User Skills
    // It requires User ID and List of Skill IDs
    [HttpPost]
    [Route("change-skills")]
    public async Task<ActionResult> ChangeSkillsAsync(long id, List<long> skillIds)
    {
        try
        {
            if(!await _userService.IsExistAsync(id))
            {
                return NotFound();
            }
            
            if(await _userService.ChangeSkillsAsync(id, skillIds))
            {
                return Ok();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    [HttpPost]
    [Route("update-profile-image")]
    public async Task<ActionResult> Image(long id, IFormFile image)
    {
        try
        {
            if(await _userService.IsExistAsync(id))
            {
                byte[] imageBytes;
                using (var item = new MemoryStream())
                {
                    image.CopyTo(item);
                    imageBytes = item.ToArray();
                }
                if(await _userService.UpdateImageAsync(id, imageBytes))
                {
                    return Ok(imageBytes);
                }
                return BadRequest();
            }
           return NotFound();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    [HttpPost]
    [Route("Download")]
    public async Task<ActionResult> Download(long id)
    {
        try
        {
            if(await _userService.IsExistAsync(id))
            {
                UserDTO user = await _userService.GetAsync(id);
                return File(user.ProfileImage, "application/octet-stream", "abc.png");

            }
           return NotFound();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }
}
