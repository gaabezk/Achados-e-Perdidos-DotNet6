using com.achadoseperdidos.Api.Data;
using com.achadoseperdidos.Api.Entities;
using com.achadoseperdidos.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace com.achadoseperdidos.Api.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApiDbContext _db;

    public ItemRepository(ApiDbContext db)
    {
        _db = db;
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        return await _db.Item.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Item>> GetAllAsync()
    {
        return await _db.Item.ToListAsync();
    }

    public async Task<Item> CreateAsync(Item item)
    {
        _db.Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task EditAsync(Item item)
    {
        _db.Update(item);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Item item)
    {
        _db.Remove(item);
        await _db.SaveChangesAsync();
    }
}