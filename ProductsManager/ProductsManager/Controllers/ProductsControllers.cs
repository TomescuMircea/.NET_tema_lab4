using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Use_Cases.Commands;

namespace ProductsManager.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsControllers:ControllerBase
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
            return Ok(id);
        }
    }
}
