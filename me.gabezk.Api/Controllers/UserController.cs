using me.gabezk.Application.Dto;
using me.gabezk.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace me.gabezk.Api.Controllers;

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
    public async Task<ActionResult> PostAsync([FromBody] UserDto userDto)
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
    [Route("{id}")]
    public async Task<ActionResult> GetAllById(int id)
    {
        var result = await _userService.GetById(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UserDto userDto)
    {
        var result = await _userService.UpdateAsync(userDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPut]
    [Route("/admin/api/role")]
    public async Task<ActionResult> UpdateRoleAsync([FromQuery] int id, string role)
    {
        var result = await _userService.EditRoleAsync(id, role);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        var result = await _userService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}