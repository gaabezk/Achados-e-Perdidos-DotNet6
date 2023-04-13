using FluentValidation;

namespace Application.Dto.Validator;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email deve ser informado!");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha deve ser informada!");
        
    }
}