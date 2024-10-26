using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductsManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductsControllers(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { Id = id }, id);

            return Ok(id);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await mediator.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The id should be identical with command.Id");
            }
            await mediator.Send(command);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await mediator.Send(new GetProductsQuery());

        }
    }
}
