using FluentValidation;

namespace me.gabezk.Application.Dto.Validations;

public class UserDTOValidator : AbstractValidator<UserDto>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Nome completo deve ser informado!");

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