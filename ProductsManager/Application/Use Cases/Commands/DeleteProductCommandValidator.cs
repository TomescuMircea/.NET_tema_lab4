using Domain.Repositories;
using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepository repository;

        public DeleteProductCommandValidator(IProductRepository repository)
        {
            this.repository = repository;

            RuleFor(x => x.Id).MustAsync(BeAValidProduct)
                              .WithMessage("Product not found");
        }

        private async Task<bool> BeAValidProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductAsync(id);
            return product != null;
        }
    }

}
