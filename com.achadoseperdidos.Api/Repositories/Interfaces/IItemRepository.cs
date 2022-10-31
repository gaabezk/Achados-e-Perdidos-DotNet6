using com.achadoseperdidos.Api.Entities;

namespace com.achadoseperdidos.Api.Repositories.Interfaces;

public interface IItemRepository
{
    Task<Item> GetByIdAsync(int id);
    Task<ICollection<Item>> GetAllAsync();
    Task<Item> CreateAsync(Item item);
    Task EditAsync(Item item);
    Task DeleteAsync(Item item);
}