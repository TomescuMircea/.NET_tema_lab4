using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateProductCommand : CreateProductCommand, IRequest
    {
        public Guid Id { get; set; }
    }
}
