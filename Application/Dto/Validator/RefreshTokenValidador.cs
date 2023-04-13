using Domain.Models.Entities;
using FluentValidation;

namespace Application.Dto.Validator;

public class RefreshTokenValidador : AbstractValidator<RefreshTokenModel>
{
    public RefreshTokenValidador()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email deve ser informado!");

        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("RefreshToken deve ser informado!");
    }
}