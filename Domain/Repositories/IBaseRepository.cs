using Domain.Models.Entities;

namespace Domain.Repositories;

public interface IBaseRepository<TEntity,TPrimaryKey>
{
    Task<TEntity> GetByIdAsync(TPrimaryKey id);
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity entity);
    Task EditAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}