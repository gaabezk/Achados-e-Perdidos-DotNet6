using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Dto;
using Application.Dto.Validator;
using Application.Services.Interfaces;
using Domain.Models.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;

    public TokenService(IWebHostEnvironment environment, IRefreshTokenRepository refreshTokenRepository, IUserService userService, IConfiguration config)
    {
        _environment = environment;
        _refreshTokenRepository = refreshTokenRepository;
        _userService = userService;
        _config = config;
    }

    public async Task<ResultService<TokenResponseDto>> AuthenticateUserAsync(LoginRequestDto loginRequestDto)
    {
        var result = _userService.Authenticate(loginRequestDto);
        if (!result.Result.IsSuccess)
            return ResultService.Fail<TokenResponseDto>(result.Result.Message);
        
        var token = GenerateToken(result.Result.Data).Result.Data;
        var refreshToken = GenerateRefreshToken().Result;
        var model = new RefreshTokenModel(loginRequestDto.Email, refreshToken);
        var result21 = await DeleteRefreshTokenAsyncByEmail(loginRequestDto.Email);
        await SaveRefreshTokenAsync(model);
        
        return ResultService.Ok(new TokenResponseDto{AccessToken = token,RefreshToken = refreshToken});
    }

    public async Task<ResultService<string>> GenerateToken(User user)
    {
        var tokenHandle = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetConnectionString("Secret") ?? throw new InvalidOperationException("Secret erro"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Email", user.Email),
                new Claim("Phone", user.Phone),
                new Claim("Role", user.Role!)
            }),
            Expires = _environment.IsDevelopment() || _environment.IsStaging()
                ? DateTime.UtcNow.AddYears(1)
                : DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandle.CreateToken(tokenDescriptor);
        return ResultService.Ok<string>(tokenHandle.WriteToken(token));
    }

    public async Task<ResultService<string>> GenerateTokenByRefreshToken(IEnumerable<Claim> claims)
    {
        var tokenHandle = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetConnectionString("Secret"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = _environment.IsDevelopment() || _environment.IsStaging()
                ? DateTime.UtcNow.AddYears(1)
                : DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandle.CreateToken(tokenDescriptor);
        return ResultService.Ok<string>(tokenHandle.WriteToken(token));
    }

    public async Task<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rnp = RandomNumberGenerator.Create();
        rnp.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetConnectionString("Secret"))),
            ValidateLifetime = false
        };

        var tokenHandle = new JwtSecurityTokenHandler();
        var principal = tokenHandle.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
    
    public async Task<ResultService<RefreshTokenModel>> SaveRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
    {
        if (refreshTokenModel == null)
            throw new Exception("o objeto deve ser informado");

        var result = new RefreshTokenValidador().Validate(refreshTokenModel);
        if (!result.IsValid)
            throw new Exception("erro de validaçao");

        return ResultService.Ok(await _refreshTokenRepository.SaveRefreshTokenAsync(refreshTokenModel));

    }

    public async Task<ResultService<RefreshTokenModel>> GetRefreshTokenAsyncByEmail(string email)
    {
        var refreshToken = _refreshTokenRepository.GetRefreshTokenByEmailAsync(email);
        if (refreshToken == null)
            ResultService.Fail("Não existe RefreshToken para este usuário!");

        return ResultService.Ok(refreshToken.Result);
    }

    public async Task<ResultService> DeleteRefreshTokenAsyncByEmail(string email)
    {
        var refreshToken = _refreshTokenRepository.GetRefreshTokenByEmailAsync(email);
        if (refreshToken.Result != null)
            await _refreshTokenRepository.DeleteRefreshTokenAsync(refreshToken.Result);
        
        return ResultService.Ok("Refresh token deletado com sucesso!");

    }
}