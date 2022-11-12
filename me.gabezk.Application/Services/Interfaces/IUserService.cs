using me.gabezk.Application.Dto;

namespace me.gabezk.Application.Services.Interfaces;

public interface IUserService
{
    Task<ResultService<UserDto>> CreateAsync(UserDto userDto);
    Task<ResultService<ICollection<UserDto>>> GetAllAsync();
    Task<ResultService<UserDto>> GetById(int id);
    Task<ResultService> UpdateAsync(UserDto pessoaDto);
    Task<ResultService> RemoveAsync(int id);
    Task<ResultService> EditRoleAsync(int id, string role);
}