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
        var data = await _userRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<UserDto>>(data));
    }

    public async Task<ResultService<UserDto>> GetById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if(user == null)
            return ResultService.Fail<UserDto>($"Usuario do id {id} nao encontrado!");

        return ResultService.Ok<UserDto>(_mapper.Map<UserDto>(user));
    }

    public async Task<ResultService> UpdateAsync(UserDto userDto)
    {
        if(userDto == null)
            return ResultService.Fail("Objeto deve ser informado!");

        var validation = new UserDTOValidator().Validate(userDto);
        if(!validation.IsValid)
            return ResultService.RequestError("Problemas com a validade dos campos",validation);

        var user = await _userRepository.GetByIdAsync(userDto.Id);
        if(user == null)
            return ResultService.Fail($"Usuario do id {userDto.Id} não foi encontrado!");

        user = _mapper.Map(userDto, user); // Edicão
        await _userRepository.EditAsync(user); 
        return ResultService.Ok($"Usuario do id {user.Id} foi editado com sucesso!");

    }

    public async Task<ResultService> RemoveAsync(int id)
    {
        var pessoa = await _userRepository.GetByIdAsync(id);
        if (pessoa == null)
            return ResultService.Fail<UserDto>("Usuario nao encontrado");

        await _userRepository.DeleteAsync(pessoa);
        return ResultService.Ok($"Usuaro do id {id} foi deletado com sucesso!");

    }
}