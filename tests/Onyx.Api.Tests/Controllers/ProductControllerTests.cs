using FluentAssertions;
using FluentAssertions.Execution;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Onyx.Api.Controllers;
using Onyx.Application.Mediator.Queries;
using Onyx.Models.Models;

namespace Onyx.Api.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly ProductsController _controller;
        private readonly ILogger<ProductsController> _logger;

        public ProductControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<ProductsController>>().Object;
            _controller = new ProductsController(_mediator.Object, _logger);
        }

        [Fact]
        public async Task AllProducts_ReturnsOk()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product", Color = "Red", },
                new Product { Id = 2, Name = "Test Product", Color = "Blue", }
            };

            _mediator.Setup(x => x.Send(It.IsAny<ProductQuery>(), default))
                .ReturnsAsync(products);
   
            var result = (OkObjectResult)await _controller.AllProducts();
          
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(200);
                result.Should().BeOfType<OkObjectResult>();
                
                _mediator.Verify(x => x.Send(It.IsAny<ProductQuery>(), default), Times.Once);
              
            }
        }

        [Fact]
        public async Task GetProductByColor_ReturnsOk()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product", Color = "Red", },
                new Product { Id = 2, Name = "Test Product", Color = "Blue", }
            };

            _mediator.Setup(x => x.Send(It.IsAny<ProductQueryByColor>(), default))
                .ReturnsAsync(products);

            var result = (OkObjectResult)await _controller.ProductByColor("Red");

            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(200);
                result.Should().BeOfType<OkObjectResult>();

                _mediator.Verify(x => x.Send(It.IsAny<ProductQueryByColor>(), default), Times.Once);

            }
        }
 
    }
}
