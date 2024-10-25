using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.VAT).GreaterThan(0);
            RuleFor(x => x.Id).NotEmpty().Must(BeAValidGuid).WithMessage("'Id' must be a valid Guid;");
        }

        private bool BeAValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }

    }
}
