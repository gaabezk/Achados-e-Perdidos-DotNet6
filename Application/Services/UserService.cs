using Application.Dto;
using Application.Dto.Validator;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Enum;
using Domain.Models.Entities;
using Domain.Repositories;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<ResultService<UserResponseDto>> CreateAsync(UserRequestDto dto)
    {
        var result = await new UserDtoValidator().ValidateAsync(dto);
        if (!result.IsValid)
            return ResultService.RequestError<UserResponseDto>("Problemas de validade", result);

        if (await CheckUserExistsByEmail(dto.Email))
            return ResultService.Fail<UserResponseDto>("Email já cadastrado!");

        var hashPassword = PasswordService.GenerateHashPassword(dto.Password);
        dto.Password = hashPassword; 
        
        var user = _mapper.Map<User>(dto); // criação
        var data = await _userRepository.CreateAsync(user);
        return ResultService.Ok(_mapper.Map<UserResponseDto>(data));
    }

    public async Task<ResultService<ICollection<UserResponseDto>>> GetAllAsync()
    {
        var data = await _userRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<UserResponseDto>>(data));
    }

    public async Task<ResultService<UserResponseDto>> GetById(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null 
            ? ResultService.Fail<UserResponseDto>($"Usuário do id {id} não foi encontrado!")
            : ResultService.Ok(_mapper.Map<UserResponseDto>(user));
    }
    
    public virtual async Task<ResultService> UpdateAsync(UserEditRequestDto dto, Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ResultService.Fail($"Usuário do id {id} não foi encontrado!");
        
        dto.Password = user.HashPassword;
        
        var result = await new UserDtoValidator().ValidateAsync(_mapper.Map<UserRequestDto>(dto));
        if (!result.IsValid)
            return ResultService.RequestError<UserRequestDto>("Problemas de validade", result);

        // if (await CheckUserExistsByEmail(dto.Email))
        //     if (!string.Equals(dto.Email, user.Email))
        //         return ResultService.Fail<UserRequestDto>("Já existe um usuário com este e-mail");
        
        // verifica se o email que desejo trocar é igual ao email antigo.
        // caso o email desejado seja diferente do antigo
        // é feita uma verificacao
        if (!string.Equals(dto.Email, user.Email))
        {
            if(await CheckUserExistsByEmail(dto.Email))
                return ResultService.Fail<UserRequestDto>("Já existe um usuário com este e-mail");
        }
        
        user = _mapper.Map(dto, user); // Edicão
        await _userRepository.EditAsync(user);
        return ResultService.Ok($"Usuário do id {user.Id} foi editado com sucesso!");
    }

    public async Task<ResultService> UpdatePassAsync(UpdatePasswordDto dto)
    {
        var result = await new UpdatePassDtoValidator().ValidateAsync(dto);
        if (!result.IsValid)
            return ResultService.RequestError<UserRequestDto>("Problemas de validade", result);
        
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            return ResultService.Fail($"Usuário do email {dto.Email} não foi encontrado!");

        var validPass = PasswordService.VerifyHashPassword(dto.OldPassword, user.HashPassword);

        if (!validPass)
            return ResultService.Fail("Senha antiga incorreta");

        user.SetHasPassword(PasswordService.GenerateHashPassword(dto.NewPassword));
        await _userRepository.EditAsync(user);
        return ResultService.Ok("Senha alterada com sucesso!");
    }

    public async Task<ResultService> RemoveAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ResultService.Fail($"Usuário do id {id} não foi encontrado!");

        await _userRepository.DeleteAsync(user);
        return ResultService.Ok($"Usuário do id {id} foi deletado com sucesso!");
    }
    
    public async Task<ResultService<User>> Authenticate(LoginRequestDto loginRequestDto)
    {
        var result = await new LoginRequestDtoValidator().ValidateAsync(loginRequestDto);
        if (!result.IsValid)
            return ResultService.RequestError<User>("Problemas de validade", result);
        
        var user = await _userRepository.GetByEmailAsync(loginRequestDto.Email);
        if (user == null)
            return ResultService.Fail<User>("E-mail incorreto");

        // if (user.Active != true)
        //     return ResultService.Fail<User>("Usuário desativado!");

        var validPass = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.HashPassword);

        return !validPass ? ResultService.Fail<User>("Senha incorreta") : ResultService.Ok(user);
    }

    public async Task<ResultService> EditRoleAsync(Guid id, string role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return ResultService.Fail($"Usuário do id {id} não foi encontrado!");

        switch (role.ToLower())
        {
            case "user":
                user.SetRole(Role.User.ToString().ToLower());
                break;
            case "admin":
                user.SetRole(Role.Admin.ToString().ToLower());
                break;
            default:
                return ResultService.Fail(" Role inválida ou nula! Passe ADMIN ou USER ");
        }
        await _userRepository.EditAsync(user);
        return ResultService.Ok("Role editada com sucesso!");
    }

    public async Task<bool> CheckUserExistsByEmail(string email)
    {
        return await _userRepository.CheckUserExistsByEmail(email);
    }
}