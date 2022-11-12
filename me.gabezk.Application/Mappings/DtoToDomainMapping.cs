using AutoMapper;
using me.gabezk.Application.Dto;
using me.gabezk.Domain.Entities;

namespace me.gabezk.Application.Mappings;

public class DtoToDomainMapping : Profile
{
    public DtoToDomainMapping()
    {
        CreateMap<UserDto, User>();
        CreateMap<PostDto, Post>();
    }
}