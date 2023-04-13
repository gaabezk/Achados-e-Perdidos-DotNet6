using Data.Context;
using Domain.Models.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly MySqlContext _db;

    public RefreshTokenRepository(MySqlContext db)
    {
        _db = db;
    }

    public async Task<RefreshTokenModel> SaveRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
    {
        _db.RefreshToken.Add(refreshTokenModel);
        await _db.SaveChangesAsync();
        return refreshTokenModel;
    }

    public async Task<RefreshTokenModel> GetRefreshTokenByEmailAsync(string email)
    {
        return await _db.RefreshToken.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task DeleteRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
    {
        _db.RefreshToken.Remove(refreshTokenModel);
        await _db.SaveChangesAsync();
    }
}