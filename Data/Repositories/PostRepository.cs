using Data.Context;
using Domain.Models.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PostRepository : IPostRepository
{    
    private readonly MySqlContext _db;

    public PostRepository(MySqlContext db)
    {
        _db = db;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        return await _db.Posts.Include(e=>e.User).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Post>> GetAllAsync()
    {
        return await _db.Posts.Include(e=>e.User).ToListAsync();
    }

    public async Task<Post> CreateAsync(Post entity)
    {
        _db.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task EditAsync(Post entity)
    {
        _db.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post entity)
    {
        _db.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<ICollection<Post>> GetAllByStatus(string status)
    {
        return await _db.Posts.Where(b => b.PostStatus.ToLower().Equals(status.ToLower())).Include(a=>a.User).ToListAsync();
    }

    public async Task<ICollection<Post>> GetAllByUserId(Guid userId)
    {
        return await _db.Posts.Where(b => b.UserId == userId).Include(a=>a.User).ToListAsync();
    }
}