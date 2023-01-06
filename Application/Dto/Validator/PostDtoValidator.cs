using FluentValidation;

namespace Application.Dto.Validator;

public class PostDtoValidator : AbstractValidator<PostRequestDto>
{
    public PostDtoValidator()
    {
        RuleFor(x => x.ItemName)
            .NotEmpty()
            .WithMessage("Nome do item deve ser informado!");
        
        RuleFor(x => x.ItemDateFound)
            .NotEmpty()
            .WithMessage("Data encontrada do item deve ser informada!");

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