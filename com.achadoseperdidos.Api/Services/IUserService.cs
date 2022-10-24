﻿using com.achadoseperdidos.Api.DTO;

namespace com.achadoseperdidos.Api.Services;

public interface IUserService
{
    Task<ResultService<UserDto>> CreateAsync(UserDto userDto);
    Task<ResultService<ICollection<UserDto>>> GetAllAsync();
    Task<ResultService<UserDto>> GetById(int id);
    Task<ResultService> UpdateAsync(UserDto pessoaDto);
    Task<ResultService> RemoveAsync(int id);
}