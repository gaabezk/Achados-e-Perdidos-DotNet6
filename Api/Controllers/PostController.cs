using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    
    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] PostRequestDto postDto)
    {
        var result = await _postService.CreateAsync(postDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult> GetAllById(Guid id)
    {
        var result = await _postService.GetById(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpGet]
    [Route("status")]
    public async Task<ActionResult> GetAllByStatus(string status)
    {
        var result = await _postService.GetAllByStatus(status);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpGet]
    [Route("user")]
    public async Task<ActionResult> GetAllByUserId(Guid id)
    {
        var result = await _postService.GetAllByUserId(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _postService.GetAllAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult> UpdateAsync([FromBody] PostEditRequestDto postDto, Guid id)
    {
        var result = await _postService.UpdateAsync(postDto, id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    [Route("status")]
    public async Task<ActionResult> UpdateRoleAsync([FromQuery] Guid id, string status)
    {
        var result = await _postService.EditStatusAsync(id, status);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult> RemoveAsync(Guid id)
    {
        var result = await _postService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}