using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public decimal VAT { get; set; }
    }
}
