using AutoMapper;
using com.achadoseperdidos.Api.DTO;
using com.achadoseperdidos.Api.Entities;

namespace com.achadoseperdidos.Api.Mappings;

public class DtoToDomainMapping : Profile
{
    public DtoToDomainMapping()
    {
        CreateMap<UserDto, User>();
    }
}