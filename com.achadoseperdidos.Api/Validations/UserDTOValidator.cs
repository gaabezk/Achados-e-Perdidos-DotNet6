using com.achadoseperdidos.Api.DTO;
using FluentValidation;

namespace com.achadoseperdidos.Api.Validations;

public class UserDTOValidator : AbstractValidator<UserDto>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Nome completooooooo deve ser informado!");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email deve ser informado!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha deve ser informado!");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Telefone deve ser informado!");
    }
}