using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ProductManagement.Application.UnitTests
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public void Given_ValidProductId_When_HandleIsCalled_Then_AProductDtoShouldBeReturned()
        {
            // Arrange
            var products = GenerateProducts();
            var product = products.First();
            var productDto = GenerateProductDto(product);

            repository.GetProductAsync(product.Id).Returns(product); // Configurare substitute pentru repo
            mapper.Map<ProductDto>(product).Returns(productDto);     // Configurare substitute pentru mapper

            var query = new GetProductByIdQuery { Id = product.Id };
            var handler = new GetProductByIdQueryHandler(repository, mapper);

            // Act
            var result = handler.Handle(query, CancellationToken.None).Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductDto>();
            result.Id.Should().Be(product.Id);
        }

        [Fact]  
        public void Given_InvalidProductId_When_HandleIsCalled_Then_NullShouldBeReturned()
        {
           
            var query = new GetProductByIdQuery { Id = Guid.NewGuid() }; 
            repository.GetProductAsync(query.Id).Returns((Product)null); 

            var handler = new GetProductByIdQueryHandler(repository, mapper);

        
            var result = handler.Handle(query, CancellationToken.None).Result;

            result.Should().BeNull();
        }

        private List<Product> GenerateProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Price = 10,
                    VAT = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Price = 20,
                    VAT = 5
                }
            };
        }

        private ProductDto GenerateProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                VAT = product.VAT
            };
        }
    }
}
