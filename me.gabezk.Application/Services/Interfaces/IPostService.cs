using me.gabezk.Application.Dto;

namespace me.gabezk.Application.Services.Interfaces;

public interface IPostService
{
    Task<ResultService<PostDtoReturn>> CreateAsync(PostDto postDto);
    Task<ResultService<ICollection<PostDtoReturn>>> GetAllAsync();
    Task<ResultService<PostDtoReturn>> GetById(int id);
    Task<ResultService> UpdateAsync(PostDto postDto);
    Task<ResultService> RemoveAsync(int id);
    Task<ResultService> EditStatusAsync(int id, string role);
}