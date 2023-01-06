using FluentValidation;

namespace Application.Dto.Validator;

public class UpdatePassDtoValidator : AbstractValidator<UpdatePasswordDto>
{
    public UpdatePassDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email deve ser informado!");
        
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("Senha antiga deve ser informada!");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Nova senha deve ser informada!");
        

    }
}