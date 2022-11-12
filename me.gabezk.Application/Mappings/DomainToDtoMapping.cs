using AutoMapper;
using me.gabezk.Application.Dto;
using me.gabezk.Domain.Entities;

namespace me.gabezk.Application.Mappings;

public class DomainToDtoMapping : Profile
{
    public DomainToDtoMapping()
    {
        CreateMap<User, UserDto>();
        CreateMap<Post, PostDto>();
        CreateMap<Post, PostDtoReturn>()
            .ForMember(x => x.User, opt => opt.Ignore())
            .ConstructUsing((model, context) => { return new PostDtoReturn(model); }
            );
    }
}