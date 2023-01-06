using Application.Dto;
using Domain.Models.Entities;

namespace Application.Services.Interfaces;

public interface IPostService : IBaseService<PostRequestDto,PostResponseDto,Guid>
{
    Task<ResultService> EditStatusAsync(Guid id, string status);
    Task<ResultService> UpdateAsync(PostEditRequestDto dto, Guid id);
    Task<ResultService<ICollection<PostResponseDto>>> GetAllByStatus(string status);
    Task<ResultService<ICollection<PostResponseDto>>> GetAllByUserId(Guid id);
}