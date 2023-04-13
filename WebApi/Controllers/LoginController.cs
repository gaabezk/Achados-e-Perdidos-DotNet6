using System.Security.Principal;
using Application.Dto;
using Application.Services;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly TokenService _tokenService;

    public LoginController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> AuthenticateUserAsync([FromBody] LoginRequestDto loginRequestDto)
    {
        var result = _tokenService.AuthenticateUserAsync(loginRequestDto);
        if (result.Result.IsSuccess)
            return Ok(result.Result.Data);

        return BadRequest(result.Result);;
    }
    
    // [HttpPost]
    // [Route("Refresh")]
    // [AllowAnonymous]
    // public async Task<ActionResult<dynamic>> Refresh(RequestRefreshToken requestRefreshToken)
    // {
    //     var principal = _tokenService.GetPrincipalFromExpiredToken(requestRefreshToken.Token);
    //     var email = principal.FindFirstValue("CorporativeEmail");
    //     var savedRefreshToken = _tokenService.GetRefreshTokenAsync(email);
    //     if (savedRefreshToken.RefreshToken != requestRefreshToken.RefreshToken)
    //         throw new SecurityTokenException("Invalid refresh!");
    //
    //     var newJwtToken = _tokenService.GenerateToken(principal.Claims);
    //     var newRefreshToken = _tokenService.GenerateRefreshToken();
    //     await _tokenService.DeleteRefresTokenAsync(email);
    //     var model = new RefreshTokenModel(email, newRefreshToken);
    //     await _tokenService.SaveRefreshTokenAsync(model);
    //
    //     return new ObjectResult(new
    //     {
    //         token = newJwtToken,
    //         refreshToken = newRefreshToken
    //     });
    // }
    
}