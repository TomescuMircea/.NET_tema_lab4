using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Product>> Handle(GetProductsQuery request,CancellationToken cancellationToken)
        {
            var products = await repository.GetProductsAsync();
            return mapper.Map<List<Product>>(products);

        }
    }
}
