using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

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
        public async Task Can_Delete_An_Existing_Product()
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

        [Fact]
        public async Task Cannot_Delete_A_NonExisting_Product()
        {
            // Arrange
            var product = GenerateOneProduct();
            repository.GetProductAsync(product.Id).Returns((Product)null);
            var command = new DeleteProductCommand { Id = product.Id };
            var handler = new DeleteProductCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Guid.Empty);
            await repository.DidNotReceive().DeleteAsync(product.Id);
        }


        [Fact]
        public async Task Cannot_Delete_A_Product_With_Empty_Id()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = Guid.Empty };
            var handler = new DeleteProductCommandHandler(repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Guid.Empty);
            await repository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
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
