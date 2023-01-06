using FluentValidation;

namespace Application.Dto.Validator;

public class UserDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Primeiro nome deve ser informado!");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Segundo nome deve ser informado!");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email deve ser informado!");
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Telefone deve ser informado!");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha deve ser informada!");
    }
}