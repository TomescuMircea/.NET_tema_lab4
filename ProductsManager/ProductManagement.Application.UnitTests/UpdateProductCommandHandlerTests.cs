using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace ProductManagement.Application.UnitTests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Given_ValidNewProduct_When_ProductExists_Then_ShouldUpdateProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = GenerateProducts().First();
            var updateCommand = new UpdateProductCommand { Id = productId, Name = "New Name", Price = 15.0m, VAT = 0.2m };
            var handler = new UpdateProductCommandHandler(repository, mapper);

            repository.GetProductAsync(productId).Returns(existingProduct);
            mapper.Map<Product>(updateCommand).Returns(new Product { Id = productId, Name = "New Name", Price = 15.0, VAT = 0.2m });

            // Act
            await handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            await repository.Received(1).UpdateAsync(Arg.Is<Product>(p => p.Id == productId && p.Name == "New Name" && p.Price == 15.0));
        }

        [Fact]
        public async Task Given_InvalidProductId_When_ProductDoesNotExist_Then_ShouldNotUpdateProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updateCommand = new UpdateProductCommand { Id = productId, Name = "Non-existent Product", Price = 20.0m, VAT = 0.3m };
            var handler = new UpdateProductCommandHandler(repository, mapper);

            repository.GetProductAsync(productId).Returns((Product)null);

            // Act
            Func<Task> act = async () => await handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            await repository.DidNotReceive().UpdateAsync(Arg.Any<Product>());
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
        

    }
}
