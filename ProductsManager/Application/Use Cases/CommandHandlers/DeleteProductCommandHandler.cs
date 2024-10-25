using Application.Use_Cases.Commands;
using Domain.Repositories;
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
            var product = await repository.GetProductAsync(request.Id);
            await repository.DeleteAsync(request.Id);
            return product.Id;
        }
    }
}
