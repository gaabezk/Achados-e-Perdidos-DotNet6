using Domain.Models.Entities;

namespace Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshTokenModel> SaveRefreshTokenAsync(RefreshTokenModel refreshTokenModel);
    Task<RefreshTokenModel> GetRefreshTokenByEmailAsync(string email);
    Task DeleteRefreshTokenAsync(RefreshTokenModel refreshTokenModel);
}