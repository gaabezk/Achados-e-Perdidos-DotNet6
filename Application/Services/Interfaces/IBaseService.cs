using Application.Dto;

namespace Application.Services.Interfaces;

public interface IBaseService<TRequest,TResponse,TPrimaryKey>
{
    Task<ResultService<TResponse>> CreateAsync(TRequest dto);
    Task<ResultService<ICollection<TResponse>>> GetAllAsync();
    Task<ResultService<TResponse>> GetById(TPrimaryKey id);
    Task<ResultService> RemoveAsync(TPrimaryKey id);
}