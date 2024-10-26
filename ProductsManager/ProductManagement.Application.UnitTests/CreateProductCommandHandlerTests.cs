using Application.Use_Cases.CommandHandlers;
using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.UnitTests
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public CreateProductCommandHandlerTests()
        {
            repository = Substitute.For<IProductRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldReturnAProduct()
        {
            // Arrange
            Product product = GenerateProduct();

            var command = new CreateProductCommand { Name = product.Name, Price = product.Price, VAT = product.VAT };
            var handler = new CreateProductCommandHandler(repository, mapper);
            
            repository.AddAsync(product).Returns(product.Id);

            mapper.Map<Product>(command).Returns(new Product { Id = product.Id, Name = product.Name, Price = product.Price, VAT = product.VAT });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(Arg.Is<Product>(p => p.Id == product.Id && p.Name == product.Name && p.Price == product.Price && p.VAT == product.VAT));
            //Assert.NotEqual(Guid.Empty, result);
            //Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProductIsNull()
        {
            // Arrange
            var command = new CreateProductCommand();
            repository.AddAsync(null).Returns(Task.FromException<Guid>(new ArgumentNullException()));

            var handler = new CreateProductCommandHandler(repository, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
        }
        private Product GenerateProduct()
        {
            // Arrange
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Price = 1000.0,
                VAT = 19
            };
        }
    }
}
