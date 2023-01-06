using Domain.Models.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository<User,Guid>
{
    Task<bool> CheckUserExistsByEmail(string email);
    Task<User> GetByEmailAsync(string email);
}