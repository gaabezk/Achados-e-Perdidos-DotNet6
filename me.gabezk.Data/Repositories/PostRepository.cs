using me.gabezk.Data.Context;
using me.gabezk.Domain.Entities;
using me.gabezk.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace me.gabezk.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApiDbContext _db;

    public PostRepository(ApiDbContext db)
    {
        _db = db;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await _db.Post.Include(e => e.User).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Post>> GetAllAsync()
    {
        return await _db.Post.Include(x => x.User).ToListAsync();
    }

    public async Task<Post> CreateAsync(Post post)
    {
        _db.Add(post);
        await _db.SaveChangesAsync();
        return post;
    }

    public async Task EditAsync(Post post)
    {
        _db.Update(post);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        _db.Remove(post);
        await _db.SaveChangesAsync();
    }
}