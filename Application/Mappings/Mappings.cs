using Application.Dto;
using AutoMapper;
using Domain.Models.Entities;

namespace Application.Mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<Post, PostEditRequestDto>().ReverseMap();
        CreateMap<PostRequestDto, PostEditRequestDto>().ReverseMap();
        CreateMap<Post, PostRequestDto>().ReverseMap();
        CreateMap<Post, PostResponseDto>()
            .ForMember(dst => dst.UserFullName,
                map => map.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dst => dst.UserPhone,
                map => map.MapFrom(src => src.User.Phone));
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<User, UserEditRequestDto>().ReverseMap();
        CreateMap<UserRequestDto, UserEditRequestDto>().ReverseMap();
        CreateMap<User, UserRequestDto>()
            .ForMember(dst => dst.Password,
                map => map.MapFrom(src => src.HasPassword));
        CreateMap<UserRequestDto, User>()
            .ForMember(dst => dst.HasPassword,
                map => map.MapFrom(src => src.Password));
    }
}