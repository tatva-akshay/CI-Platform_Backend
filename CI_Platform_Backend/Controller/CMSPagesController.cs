using System.Net;
using AutoMapper;
using CI_Platform_Backend_DBEntity.DataModels;
using CI_Platform_Backend_Presentation.DTO.CMSPages;
using CI_Platform_Backend_Services.CMSPage;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controller;

[ApiController]
[Route("cms-pages")]
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
    [Route("")]
    public async Task<ActionResult> CreateAsync(CreateCMSPageDTO cMSPageDTO)
    {
        return await _cMSPageService.AddAsync(_mapper.Map<CmsPrivacyPolicy>(cMSPageDTO)) ?
            Ok() :
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Update CMS Page
    [HttpPut]
    [Route("")]
    public async Task<ActionResult> UpdateAsync(long id, CreateCMSPageDTO cMSPageDTO)
    {
        return await _cMSPageService.UpdateAsync(id, cMSPageDTO) ? 
            Ok() :
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Delete CMS Page
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        return await _cMSPageService.DeleteAsync(id) ?
            Ok() :
            BadRequest();
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get All CMS Pages List
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(CMSPageDTO))]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        return Ok(_mapper.Map<List<CMSPageDTO>>(await _cMSPageService.GetCMSPagesAsync()));
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get CMS Page using CMS id
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAsync(long id)
    {
        CMSPageDTO cMSPageDTO = _mapper.Map<CMSPageDTO>(await _cMSPageService.GetCMSPageAsync(id));

        return cMSPageDTO == null || cMSPageDTO.CMSPageID == 0 ?
            NotFound() :
            Ok(cMSPageDTO);
    }
}