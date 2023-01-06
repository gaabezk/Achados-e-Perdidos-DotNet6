using Application.Dto;
using Application.Dto.Validator;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Enum;
using Domain.Models.Entities;
using Domain.Repositories;

namespace Application.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IMapper mapper, IPostRepository postRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<ResultService<PostResponseDto>> CreateAsync(PostRequestDto dto)
    {
        var result = await new PostDtoValidator().ValidateAsync(dto);
        if (!result.IsValid)
            return ResultService.RequestError<PostResponseDto>("Problemas de validade", result);

        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            return ResultService.Fail<PostResponseDto>($"Usuario do id '{dto.UserId.ToString()}' nao foi encontrado!");
        
        dto.PostStatus = PostStatus.WaitingApproval.ToString();
        dto.LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);
        dto.CreationDate = DateOnly.FromDateTime(DateTime.Now);

        var post = _mapper.Map<Post>(dto); // criação
        var data = await _postRepository.CreateAsync(post);
        return ResultService.Ok(_mapper.Map<PostResponseDto>(data));
    }

    public async Task<ResultService<ICollection<PostResponseDto>>> GetAllAsync()
    {
        var data = await _postRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<PostResponseDto>>(data));

    }

    public async Task<ResultService<PostResponseDto>> GetById(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        return post == null 
            ? ResultService.Fail<PostResponseDto>($"Post do id {id} não foi encontrado!")
            : ResultService.Ok(_mapper.Map<PostResponseDto>(post));
    }

    public async Task<ResultService> UpdateAsync(PostEditRequestDto dto, Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return ResultService.Fail($"Post do id {id} não foi encontrado!");
        
        dto.LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);

        var result = await new PostDtoValidator().ValidateAsync(_mapper.Map<PostRequestDto>(dto));
        if (!result.IsValid)
            return ResultService.RequestError<PostRequestDto>("Problemas de validade", result);
        
        post = _mapper.Map(dto, post); // Edicão
        await _postRepository.EditAsync(post);
        return ResultService.Ok($"Post do id {id} foi editado com sucesso!");

    }

    public async Task<ResultService> RemoveAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return ResultService.Fail($"Post do id {id} não foi encontrado!");

        await _postRepository.DeleteAsync(post);
        return ResultService.Ok($"Post do id {id} foi deletado com sucesso!");
    }

    public async Task<ResultService> EditStatusAsync(Guid id, string status)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return ResultService.Fail($"Post do id {id} não foi encontrado!");

        switch (status.ToLower())
        {
            case "aprovado":
                post.SetPostStatus(PostStatus.Approved.ToString());
                break;
            case "recusado":
                post.SetPostStatus(PostStatus.Refused.ToString());
                break;
            case "devoldido":
                post.SetPostStatus(PostStatus.Returned.ToString());
                break;
            case "aguardando":
                post.SetPostStatus(PostStatus.WaitingApproval.ToString());
                break;
            default:
                return ResultService.Fail(
                    " Status inválido ou nulo. Passe: 'aprovado', 'recusado', 'devolvido' ou 'aguardando' ");
        }

        await _postRepository.EditAsync(post);
        return ResultService.Ok($"Status do post {id} definido para {status.ToLower()}");
    }
    
    public async Task<ResultService<ICollection<PostResponseDto>>> GetAllByStatus(string status)
    {
        var posts = await _postRepository.GetAllByStatus(status);
        return ResultService.Ok(_mapper.Map<ICollection<PostResponseDto>>(posts));
    }
    
    public async Task<ResultService<ICollection<PostResponseDto>>> GetAllByUserId(Guid id)
    {
        var posts = await _postRepository.GetAllByUserId(id);
        return ResultService.Ok(_mapper.Map<ICollection<PostResponseDto>>(posts));
    }
    
}