using com.achadoseperdidos.Api.DTO;
using FluentValidation;

namespace com.achadoseperdidos.Api.Validations;

public class UserDTOValidator : AbstractValidator<UserDto>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nome completo deve ser informado!");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email deve ser informado!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Senha deve ser informado!");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage("Telefone deve ser informado!");
        
    }
}