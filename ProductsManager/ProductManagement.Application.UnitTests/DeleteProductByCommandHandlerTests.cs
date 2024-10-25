using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagement.Application.UnitTests
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public DeleteProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Can_delete_product()
        {
            // Arrange
            var product = GenerateOneProduct();
            repository.GetProductAsync(product.Id).Returns(product);
            var command = new DeleteProductCommand { Id = product.Id };
            var handler = new DeleteProductCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(product.Id);
            await repository.Received(1).DeleteAsync(product.Id);
        }

        private Product GenerateOneProduct()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 100,
                VAT = 10
            };
        }
    }
}
