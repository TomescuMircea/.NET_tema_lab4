using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
            RuleFor(x => x.VAT).NotEmpty().GreaterThan(0);
        }
    }
}
