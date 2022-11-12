using me.gabezk.Domain.Entities;

namespace me.gabezk.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<ICollection<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task EditAsync(User user);
    Task DeleteAsync(User user);
    Task<int> GetIdByEmail(string email);
}