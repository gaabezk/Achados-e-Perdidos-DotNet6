using com.achadoseperdidos.Api.DTO;
using FluentValidation;

namespace com.achadoseperdidos.Api.Validations;

public class PostDTOValidator : AbstractValidator<PostDto>
{
    public PostDTOValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .WithMessage("Email do usuario deve ser informado!");
        
        RuleFor(x => x.ItemName)
            .NotEmpty()
            .WithMessage("Nome do item deve ser informado!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Decrição deve ser informada!");

        RuleFor(x => x.FoundLocation)
            .NotEmpty()
            .WithMessage("Lugar encontrado deve ser informado!");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Cidade deve ser informada!");
    }
}