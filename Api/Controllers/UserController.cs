using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] UserRequestDto userDto)
    {
        var result = await _userService.CreateAsync(userDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult> GetAllById(Guid id)
    {
        var result = await _userService.GetById(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<ActionResult> UpdateAsync([FromBody] UserEditRequestDto userDto, Guid id)
    {
        var result = await _userService.UpdateAsync(userDto,id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    [HttpPut]
    [Route("pass")]
    public async Task<ActionResult> UpdatePassAsync([FromBody] UpdatePasswordDto dto)
    {
        var result = await _userService.UpdatePassAsync(dto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    [Route("role")]
    public async Task<ActionResult> UpdateRoleAsync([FromQuery] Guid id, string role)
    {
        var result = await _userService.EditRoleAsync(id, role);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<ActionResult> RemoveAsync(Guid id)
    {
        var result = await _userService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}