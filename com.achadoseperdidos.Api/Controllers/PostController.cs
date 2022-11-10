using com.achadoseperdidos.Api.DTO;
using com.achadoseperdidos.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace com.achadoseperdidos.Api.Controllers;

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
    public async Task<ActionResult> PostAsync([FromBody] PostDto postDto)
    {
        var result = await _postService.CreateAsync(postDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetAllById(int id)
    {
        var result = await _postService.GetById(id);
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
    public async Task<ActionResult> UpdateAsync([FromBody] PostDto postDto)
    {
        var result = await _postService.UpdateAsync(postDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpPut]
    [Route("/admin/api/status")]
    public async Task<ActionResult> UpdateRoleAsync([FromQuery] int id, string status)
    {
        var result = await _postService.EditStatusAsync(id, status);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        var result = await _postService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}