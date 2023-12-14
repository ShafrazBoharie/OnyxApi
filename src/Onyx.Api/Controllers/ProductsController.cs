using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Application.Mediator.Queries;

namespace Onyx.Api.Controllers
{
    [ApiController]
    [Route("api/Products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator , ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> AllProducts()
        {
            try
            {
                var products = await _mediator.Send(new ProductQuery());
                _logger.LogInformation("All products endpoint called");
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);

            }
            
        }

        [HttpGet("{color}")]
        public async Task<IActionResult> ProductByColor(string color)
        {
            try
            {
                var productsByColor = await _mediator.Send(new ProductQueryByColor(color));
                _logger.LogInformation("Products by color endpoint called");
                return Ok(productsByColor);
            }
            catch (Exception e) 
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }
    }
}
