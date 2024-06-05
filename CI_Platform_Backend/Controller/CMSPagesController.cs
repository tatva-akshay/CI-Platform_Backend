using AutoMapper;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.CMSPages;
using CI_Platform_Backend_Services.CMSPage;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("api/[controller]")]
public class CMSPagesController : ControllerBase
{
    private readonly ICMSPageService _cMSPageService;

    private readonly IMapper _mapper;

    public CMSPagesController(ICMSPageService cMSPageService, IMapper mapper)
    {
        _cMSPageService = cMSPageService;
        _mapper = mapper;
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Create CMS Page
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateAsync(CreateCMSPageDTO cMSPageDTO)
    {
        if(await _cMSPageService.AddAsync(_mapper.Map<CmsPrivacyPolicy>(cMSPageDTO)))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Update CMS Page
    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateAsync(long id, CreateCMSPageDTO cMSPageDTO)
    {
        if(await _cMSPageService.UpdateAsync(id, cMSPageDTO))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Delete CMS Page
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        if(await _cMSPageService.DeleteAsync(id))
        {
            return Ok();
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get All CMS Pages List
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        try
        {
            return Ok(_mapper.Map<List<CMSPageDTO>>(await _cMSPageService.GetCMSPagesAsync()));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get CMS Page using CMS id
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAsync(long id)
    {
        try
        {
            CMSPageDTO cMSPageDTO = _mapper.Map<CMSPageDTO>(await _cMSPageService.GetCMSPageAsync(id));
            return cMSPageDTO == null || cMSPageDTO.CMSPageID == 0 ?
                NotFound() :
                Ok(cMSPageDTO);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return BadRequest();
    }
}
