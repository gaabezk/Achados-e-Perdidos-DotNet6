using AutoMapper;
using com.achadoseperdidos.Api.DTO;
using com.achadoseperdidos.Api.Entities;
using com.achadoseperdidos.Api.Repositories;
using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ResultService<UserDto>> CreateAsync(UserDto userDto)
    {
        if (userDto == null)
            return ResultService.Fail<UserDto>("Objeto deve ser informado");
        
        var result = new UserDTOValidator().Validate(userDto);
        if (!result.IsValid)
            return ResultService.RequestError<UserDto>("Problemas de validade", result);

        var user = _mapper.Map<User>(userDto); // criação
        var data = await _userRepository.CreateAsync(user);
        return ResultService.Ok(_mapper.Map<UserDto>(data));
    }

    public async Task<ResultService<ICollection<UserDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ResultService<UserDto>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultService> UpdateAsync(UserDto pessoaDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultService> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }
}