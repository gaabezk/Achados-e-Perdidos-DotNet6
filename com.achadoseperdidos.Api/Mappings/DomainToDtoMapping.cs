using AutoMapper;
using com.achadoseperdidos.Api.DTO;
using com.achadoseperdidos.Api.Entities;

namespace com.achadoseperdidos.Api.Mappings;

public class DomainToDtoMapping : Profile
{
    public DomainToDtoMapping()
    {
        CreateMap<User, UserDto>();
        CreateMap<Post, PostDto>();
        CreateMap<Post, PostDtoReturn>()
            .ForMember(x => x.User, opt => opt.Ignore())
            .ConstructUsing((model, context) =>
                {
                    return new PostDtoReturn(model);
                }
            );
    }
}