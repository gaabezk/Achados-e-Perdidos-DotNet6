using Application.Dto;
using Domain.Models.Entities;

namespace Application.Services.Interfaces;

public interface IUserService : IBaseService<UserRequestDto,UserResponseDto,Guid>
{
    Task<ResultService<User>> Authenticate(LoginRequestDto loginRequestDto);
    Task<ResultService> EditRoleAsync(Guid id, string role);
    Task<bool> CheckUserExistsByEmail(string email);
    Task<ResultService> UpdateAsync(UserEditRequestDto dto, Guid id);
    Task<ResultService> UpdatePassAsync(UpdatePasswordDto dto);

}