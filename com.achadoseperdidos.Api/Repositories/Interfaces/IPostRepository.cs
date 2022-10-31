using com.achadoseperdidos.Api.Entities;

namespace com.achadoseperdidos.Api.Repositories.Interfaces;

public interface IPostRepository
{
    Task<Post> GetByIdAsync(int id);
    Task<ICollection<Post>> GetAllAsync();
    Task<Post> CreateAsync(Post post);
    Task EditAsync(Post post);
    Task DeleteAsync(Post post);
}