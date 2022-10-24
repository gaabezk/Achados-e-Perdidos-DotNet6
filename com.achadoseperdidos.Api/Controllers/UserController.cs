using com.achadoseperdidos.Api.DTO;
using com.achadoseperdidos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace com.achadoseperdidos.Api.Controllers;

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
}