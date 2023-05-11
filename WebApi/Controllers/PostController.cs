using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    
    /// <summary>
    /// CADASTRA UM POST NO SISTEMA PASSANDO O ID DO USUÁRIO.
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> PostAsync([FromBody] PostRequestDto postDto)
    {
        var result = await _postService.CreateAsync(postDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// BUSCA UM POST PELO ID.
    /// </summary>
    [HttpGet]
    [Route("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await _postService.GetById(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// BUSCA POSTS PELO STATUS.
    /// </summary>
    [HttpGet]
    [Route("status")]
    [Authorize]
    public async Task<ActionResult> GetAllByStatus(string status)
    {
        var result = await _postService.GetAllByStatus(status);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// BUSCA POSTS PELO ID DO USUÁRIO.
    /// </summary>
    [HttpGet]
    [Route("user")]
    [Authorize]
    public async Task<ActionResult> GetAllByUserId(Guid id)
    {
        var result = await _postService.GetAllByUserId(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// LISTA TODOS OS POSTS.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _postService.GetAllAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// EDITA OS DADOS DE UM POST PELO ID.
    /// </summary>
    [HttpPut]
    [Route("update")]
    [Authorize]
    public async Task<ActionResult> UpdateAsync([FromBody] PostEditRequestDto postDto,[FromQuery] Guid idPost,[FromQuery] Guid idUser)
    {
        var result = await _postService.UpdateAsync(postDto, idPost,idUser);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// EDITA OS STATUS DE UM POST PELO ID.
    /// </summary>
    [HttpPut]
    [Route("status")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> UpdateRoleAsync([FromQuery] Guid id, string status)
    {
        var result = await _postService.EditStatusAsync(id, status);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// REMOVE UM POST DO BANCO DE DADOS PELO ID.
    /// </summary>
    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> RemoveAsync(Guid id)
    {
        var result = await _postService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}