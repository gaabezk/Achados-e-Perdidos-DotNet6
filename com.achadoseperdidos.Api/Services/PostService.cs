using AutoMapper;
using com.achadoseperdidos.Api.DTO;
using com.achadoseperdidos.Api.Entities;
using com.achadoseperdidos.Api.Enum;
using com.achadoseperdidos.Api.Repositories.Interfaces;
using com.achadoseperdidos.Api.Services.Interfaces;
using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public PostService(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<ResultService<PostDtoReturn>> CreateAsync(PostDto postDto)
    {
        if (postDto == null)
            return ResultService.Fail<PostDtoReturn>("Objeto deve ser informado");
        
        var result = new PostDTOValidator().Validate(postDto);
        if (!result.IsValid)
            return ResultService.RequestError<PostDtoReturn>("Problemas de validade", result);

        if (postDto.ItemDateFound.ToString() == "01/01/0001")
            postDto.ItemDateFound = null;

        postDto.PostStatus = PostStatus.WAITING_APPROVAL.ToString();
        postDto.LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);
        postDto.CreationDate = DateOnly.FromDateTime(DateTime.Now);
        
        var post = _mapper.Map<Post>(postDto); // criação
        var data = await _postRepository.CreateAsync(post);
        return ResultService.Ok(_mapper.Map<PostDtoReturn>(data));
    }

    public async Task<ResultService<ICollection<PostDtoReturn>>> GetAllAsync()
    {
        var data = await _postRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<PostDtoReturn>>(data));
    }

    public async Task<ResultService<PostDtoReturn>> GetById(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return ResultService.Fail<PostDtoReturn>($"Post do id {id} nao encontrado!");
        
        return ResultService.Ok(_mapper.Map<PostDtoReturn>(post));
    }

    public async Task<ResultService> UpdateAsync(PostDto postDto)
    {
        if (postDto == null)
            return ResultService.Fail("Objeto deve ser informado!");
        
        var post = await _postRepository.GetByIdAsync(postDto.Id);
        if (post == null)
            return ResultService.Fail($"Post do id {postDto.Id} não foi encontrado!");

        if (string.IsNullOrEmpty(postDto.ItemName)) 
            postDto.ItemName = post.ItemName;

        if (string.IsNullOrEmpty(postDto.Description))
            postDto.Description = post.Description;
        
        if (string.IsNullOrEmpty(postDto.ImageUrl1))
            postDto.ImageUrl1 = post.ImageUrl1;
        
        if (string.IsNullOrEmpty(postDto.ImageUrl2))
            postDto.ImageUrl2 = post.ImageUrl2;
        
        if (string.IsNullOrEmpty(postDto.ImageUrl3))
            postDto.ImageUrl3 = post.ImageUrl3;
        
        if (string.IsNullOrEmpty(postDto.Color))
            postDto.Color = post.Color;
        
        if (string.IsNullOrEmpty(postDto.City))
            postDto.City = post.City;
        
        if (string.IsNullOrEmpty(postDto.FoundLocation))
            postDto.FoundLocation = post.FoundLocation;
        
        if (string.IsNullOrEmpty(postDto.ItemDateFound.ToString()))
           postDto.ItemDateFound = post.ItemDateFound;

        postDto.CreationDate = post.CreationDate;
        postDto.PostStatus = PostStatus.WAITING_APPROVAL.ToString();
        postDto.LastUpdateDate = DateOnly.FromDateTime(DateTime.Now);
        
        post = _mapper.Map(postDto, post); // Edicão
        await _postRepository.EditAsync(post);
        return ResultService.Ok($"Post do id {post.Id} foi editado com sucesso!");

    }

    public async Task<ResultService> RemoveAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return ResultService.Fail<UserDto>("Post nao encontrado");

        await _postRepository.DeleteAsync(post);
        return ResultService.Ok($"Post do id {id} foi deletado com sucesso!");
    }

    public async Task<ResultService> EditStatusAsync(int id, string status)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
            return ResultService.Fail($"Post do id {id} não foi encontrado!");

        switch (status)
        {
            case "aprovado":
                post.SetPostStatus(PostStatus.APPROVED.ToString());
                break;
            case "recusado":
                post.SetPostStatus(PostStatus.REFUSED.ToString());
                break;
            case "devoldido":
                post.SetPostStatus(PostStatus.RETURNED.ToString());
                break;
            case "aguardando":
                post.SetPostStatus(PostStatus.WAITING_APPROVAL.ToString());
                break;
            default:
                return ResultService.Fail(" Status inválido ou nulo. Passe: 'aprovado', 'recusado', 'devolvido' ou 'aguardando' ");
        }

        await _postRepository.EditAsync(post);
        return ResultService.Ok($"Post do id {id} foi editado com sucesso!");
    }
}