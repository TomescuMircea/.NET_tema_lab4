using MediatR;

namespace Application.Use_Cases.Commands
{
    public class DeleteProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
