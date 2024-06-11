using System.Net;
using AutoMapper;
using CI_Platform_Backend_DBEntity.DbModels;
using CI_Platform_Backend_Presentation;
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
    private readonly APIResponse _aPIResponse = new APIResponse();

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
        if(await _cMSPageService.IsExistsAsync(0, cMSPageDTO.Slug))
        {
            _aPIResponse.StatusCode = HttpStatusCode.BadRequest;
            _aPIResponse.ErrorMessages = ["Page with same slug already exists. Please use another name of the slug"];
            return BadRequest(_aPIResponse);
        }
        if(await _cMSPageService.AddAsync(_mapper.Map<CmsPrivacyPolicy>(cMSPageDTO)))
        {
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_aPIResponse);
        }
        _aPIResponse.StatusCode = HttpStatusCode.BadRequest;
        _aPIResponse.ErrorMessages = ["Invalid CMS Page Data"];
        return BadRequest(_aPIResponse);
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Update CMS Page
    [HttpPut]
    [Route("")]
    public async Task<ActionResult> UpdateAsync(long id, CreateCMSPageDTO cMSPageDTO)
    {
        if(!await _cMSPageService.IsExistsAsync(id))
        {
            _aPIResponse.StatusCode = HttpStatusCode.NotFound;
            _aPIResponse.ErrorMessages = ["Invalid CMS Page Id"];
            return BadRequest(_aPIResponse);
        }
        if(await _cMSPageService.IsExistsAsync(id, cMSPageDTO.Slug))
        {
            _aPIResponse.StatusCode = HttpStatusCode.BadRequest;
            _aPIResponse.ErrorMessages = ["Page with same slug already exists. Please use another name of the slug"];
            return BadRequest(_aPIResponse);
        }
        if(await _cMSPageService.UpdateAsync(id, cMSPageDTO))
        {
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_aPIResponse);
        }
        _aPIResponse.StatusCode = HttpStatusCode.BadRequest;
        _aPIResponse.ErrorMessages = ["Invalid CMS Page Data"];
        return BadRequest(_aPIResponse);
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Delete CMS Page
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        if (!await _cMSPageService.IsExistsAsync(id))
        {
            _aPIResponse.StatusCode = HttpStatusCode.NotFound;
            _aPIResponse.ErrorMessages = ["Invalid CMS Page Id"];
            return BadRequest(_aPIResponse);
        }

        if(await _cMSPageService.DeleteAsync(id))
        {
            _aPIResponse.IsSuccess = true;
            _aPIResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_aPIResponse);
        }

        _aPIResponse.StatusCode = HttpStatusCode.BadRequest;
        _aPIResponse.ErrorMessages = ["Something went wrong. Please Try again."];
        return BadRequest(_aPIResponse);
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get All CMS Pages List
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(CMSPageDTO))]
    [Route("")]
    public async Task<ActionResult> GetAllAsync()
    {
        List<CMSPageDTO> cMSPageDTOs = _mapper.Map<List<CMSPageDTO>>(await _cMSPageService.GetCMSPagesAsync());
        _aPIResponse.IsSuccess = true;
        _aPIResponse.StatusCode = HttpStatusCode.OK;
        _aPIResponse.Result = cMSPageDTOs;
        return Ok(_aPIResponse);
    }

    // Created: 5 June - Dhruvil Bhojani
    // This Action will be used to Get CMS Page using CMS id
    // [HttpGet]
    // [Route("{id}")]
    // public async Task<ActionResult> GetAsync(long id)
    // {
    //     CMSPageDTO cMSPageDTO = _mapper.Map<CMSPageDTO>(await _cMSPageService.GetCMSPageAsync(id));

    //     return cMSPageDTO == null || cMSPageDTO.CMSPageID == 0 ?
    //         NotFound() :
    //         Ok(cMSPageDTO);
    // }

    // Created: 10 June - Dhruvil Bhojani
    // This Action will be used to Get CMS Page using Slug
    [HttpGet]
    [Route("{slug}")]
    public async Task<ActionResult> GetAsync(string slug)
    {
        CMSPageDTO cMSPageDTO = _mapper.Map<CMSPageDTO>(await _cMSPageService.GetCMSPageAsync(slug));

        if(cMSPageDTO == null || cMSPageDTO.CMSPageID == 0)
        {
            _aPIResponse.StatusCode = HttpStatusCode.NotFound;
            _aPIResponse.ErrorMessages = ["No CMS Page found for "+slug];
            return NotFound(_aPIResponse);
        }
        _aPIResponse.IsSuccess = true;
        _aPIResponse.StatusCode = HttpStatusCode.OK;
        _aPIResponse.Result = cMSPageDTO;
        return Ok(_aPIResponse);
    }
}