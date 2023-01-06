using Domain.Models.Entities;

namespace Domain.Repositories;

public interface IPostRepository : IBaseRepository<Post,Guid>
{
    Task<ICollection<Post>> GetAllByStatus(string status);
    Task<ICollection<Post>> GetAllByUserId(Guid userId);
}