﻿using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
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
    }
}
