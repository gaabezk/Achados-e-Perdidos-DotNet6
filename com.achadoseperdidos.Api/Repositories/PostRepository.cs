using com.achadoseperdidos.Api.Data;
using com.achadoseperdidos.Api.Entities;
using com.achadoseperdidos.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace com.achadoseperdidos.Api.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApiDbContext _db;

    public PostRepository(ApiDbContext db)
    {
        _db = db;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await _db.Post.FirstOrDefaultAsync(x => x.Id == id);
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