using Application.Use_Cases.Commands;
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
            //rand de decomentat dupa ce se face GetProductById
            //return CreatedAtAction(nameof(GetProductById), new { Id = id }, id);
            //rand de comentat dupa ce se face GetProductById
            return Ok(id);
        }

    }
}
