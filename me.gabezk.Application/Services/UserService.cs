using AutoMapper;
using me.gabezk.Application.Dto;
using me.gabezk.Application.Dto.Validations;
using me.gabezk.Application.Services.Interfaces;
using me.gabezk.Domain.Entities;
using me.gabezk.Domain.Enum;
using me.gabezk.Domain.Interfaces;

namespace me.gabezk.Application.Services;

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
        if (user == null)
            return ResultService.Fail<UserDto>($"Usuario do id {id} nao encontrado!");

        return ResultService.Ok(_mapper.Map<UserDto>(user));
    }

    public async Task<ResultService> UpdateAsync(UserDto userDto)
    {
        if (userDto == null)
            return ResultService.Fail("Objeto deve ser informado!");

        var user = await _userRepository.GetByIdAsync(userDto.Id);
        if (user == null)
            return ResultService.Fail($"Usuario do id {userDto.Id} não foi encontrado!");

        if (string.IsNullOrEmpty(userDto.FullName))
            userDto.FullName = user.FullName;

        if (string.IsNullOrEmpty(userDto.Email))
            userDto.Email = user.Email;

        if (string.IsNullOrEmpty(userDto.Phone))
            userDto.Phone = user.Phone;

        if (string.IsNullOrEmpty(userDto.Password))
            userDto.Password = user.Password;

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
        return ResultService.Ok($"Usuario do id {id} foi deletado com sucesso!");
    }

    public async Task<ResultService> EditRoleAsync(int id, string role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ResultService.Fail($"Usuario do id {id} não foi encontrado!");

        switch (role)
        {
            case "admin":
                user.setRole(Role.ADMIN.ToString());
                break;
            case "user":
                user.setRole(Role.USER.ToString());
                break;
            default:
                return ResultService.Fail(" Role inválida ou nula. Passe: 'admin' ou 'user' ");
        }

        await _userRepository.EditAsync(user);
        return ResultService.Ok($"Usuario do id {id} foi editado com sucesso!");
    }
}