using AutoMapper;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.CreateSkill;
using CI_Platform_Backend_Presentation.DTO.Skill;
using CI_Platform_Backend_Services.Skill;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    private readonly IMapper _mapper;

    public SkillController(ISkillService skillService, IMapper mapper)
    {
        _skillService = skillService;
        _mapper = mapper;
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Create new Skill option
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateAsync(CreateSkillDTO skillDTO)
    {
        if(await _skillService.AddAsync(_mapper.Map<Skill>(skillDTO)))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Update Skill option
    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(long id, CreateSkillDTO skillDTO)
    {
        if(await _skillService.UpdateAsync(id, skillDTO))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Delete Skill option
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        if(await _skillService.DeleteAsync(id))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get All Skill options
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            return Ok(_mapper.Map<List<SkillDTO>>(await _skillService.GetSkillsAsync()));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get Skill option using Skill ID
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAsync(long id)
    {
        try
        {
            SkillDTO skill = _mapper.Map<SkillDTO>(await _skillService.GetSkillAsync(id));
            return skill == null || skill.SkillID == 0 ?
                NotFound() :
                Ok(skill);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }
}
