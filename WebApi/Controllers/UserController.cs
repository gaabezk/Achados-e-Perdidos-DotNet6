using Application.Dto;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// CADASTRA UM USUÁRIO NO SISTEMA.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> PostAsync([FromBody] UserRequestDto userDto)
    {
        var result = await _userService.CreateAsync(userDto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// LISTA TODOS OS USUÁRIOS DO SISTEMA.
    /// </summary>
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// BUSCA UM USUÁRIO PELO ID.
    /// </summary>
    [HttpGet]
    [Route("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await _userService.GetById(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// EDITA OS DADOS DE UM USUÁRIO PELO ID.
    /// </summary>
    [HttpPut]
    [Route("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> UpdateAsync([FromBody] UserEditRequestDto userDto, Guid id)
    {
        var result = await _userService.UpdateAsync(userDto,id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// EDITA A SENHA DE UM USUARIO PELO EMAIL E SENHA ANTIGA.
    /// </summary>
    [HttpPut]
    [Route("pass")]
    [Authorize]
    public async Task<ActionResult> UpdatePassAsync([FromBody] UpdatePasswordDto dto)
    {
        var result = await _userService.UpdatePassAsync(dto);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// EDITA A ROLE DE UM USUÁRIO PELO ID.
    /// </summary>
    [HttpPut]
    [Route("role")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> UpdateRoleAsync([FromQuery] Guid id, string role)
    {
        var result = await _userService.EditRoleAsync(id, role);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// REMOVE UM USUARIO DO BANCO DE DADOS.
    /// </summary>
    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult> RemoveAsync(Guid id)
    {
        var result = await _userService.RemoveAsync(id);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}