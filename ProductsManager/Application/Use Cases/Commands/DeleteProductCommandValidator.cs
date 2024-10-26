using Domain.Repositories;
using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepository repository;

        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().Must(BeAValidGuid).WithMessage("'Id' must be a valid Guid;");
        }

        private bool BeAValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }
    }

}
