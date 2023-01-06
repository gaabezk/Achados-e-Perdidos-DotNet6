using Data.Context;
using Domain.Models.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MySqlContext _db;

    public UserRepository(MySqlContext db)
    {
        _db = db;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return (await _db.Users.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task<User> CreateAsync(User entity)
    {
        _db.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task EditAsync(User entity)
    {
        _db.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(User entity)
    {
        _db.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> CheckUserExistsByEmail(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Email == email) != null;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return (await _db.Users.FirstOrDefaultAsync(x => x.Email == email));
    }
}