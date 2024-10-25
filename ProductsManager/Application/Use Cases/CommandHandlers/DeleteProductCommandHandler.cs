using Application.Use_Cases.Commands;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IProductRepository repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            DeleteProductCommandValidator validatorRules = new DeleteProductCommandValidator();
            var validator = validatorRules.Validate(request);
            if (!validator.IsValid)
            {
                throw new ValidationException(validator.Errors);
            }

            var product = await repository.GetProductAsync(request.Id);
            await repository.DeleteAsync(request.Id);
            return product.Id;
        }
    }
}
