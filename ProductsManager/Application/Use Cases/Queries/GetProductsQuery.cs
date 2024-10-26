using Domain.Entities;
using MediatR;
namespace Application.Use_Cases.Queries
{
    public class GetProductsQuery : IRequest<List<Product>>
    {

    }
}
