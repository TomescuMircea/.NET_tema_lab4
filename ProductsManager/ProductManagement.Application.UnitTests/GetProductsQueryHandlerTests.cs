using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace ProductManagementUnitTests
{
    public class GetProductsQueryHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public GetProductsQueryHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            repository.GetProductsAsync().Returns(new List<Product>());
            mapper.Map<List<Product>>(Arg.Any<List<Product>>()).Returns(new List<Product>());

            var query = new GetProductsQuery();
            var handler = new GetProductsQueryHandler(repository, mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedListOfProducts_WhenProductsExist()
        {
            var products = GenerateProducts();

            repository.GetProductsAsync().Returns(products);
            mapper.Map<List<Product>>(products).Returns(products);

            var handler = new GetProductsQueryHandler(repository, mapper);
            var query = new GetProductsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeEquivalentTo(products);
        }


        private void GenerateProductDTO(List<Product> products)
        {
            mapper.Map<List<Product>>(Arg.Any<List<Product>>()).Returns(new List<Product>
            {
                new Product { Id = products.First().Id, Name = products.First().Name, Price = products.First().Price, VAT=products.First().VAT },
                new Product { Id = products.Last().Id, Name = products.Last().Name, Price=products.Last().Price, VAT= products.Last().VAT }
            });
        }

        private List<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Laptop" ,Price=10000, VAT=19},
                new Product { Id = Guid.NewGuid(), Name = "Smartphone",Price=1200, VAT=5}
            };
        }
    }
}
