using me.gabezk.Data.Context;
using me.gabezk.Domain.Entities;
using me.gabezk.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace me.gabezk.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApiDbContext _db;

    public UserRepository(ApiDbContext db)
    {
        _db = db;
    }

    public async Task<User> CreateAsync(User user)
    {
        _db.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _db.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<int> GetIdByEmail(string email)
    {
        return (await _db.User.FirstOrDefaultAsync(x => x.Email == email))?.Id ??
               0; // se tiver informacao retorno Id se nao retorna 0
    }

    public async Task EditAsync(User user)
    {
        _db.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        return await _db.User.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _db.User.FirstOrDefaultAsync(x => x.Id == id);
    }
}