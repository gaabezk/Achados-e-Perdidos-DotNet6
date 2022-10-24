using com.achadoseperdidos.Api.Entities;

namespace com.achadoseperdidos.Api.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<ICollection<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task EditAsync(User user);
    Task DeleteAsync(User user);
}