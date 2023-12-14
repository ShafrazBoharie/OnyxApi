using FluentAssertions;
using Moq;
using Onyx.Application.Mediator;
using Onyx.Application.Mediator.Queries;
using Onyx.Application.Services;
using Onyx.Models.Models;

namespace Onyx.Application.Tests.Mediator
{
    public class GetProductHandlerMediatorTests
    {
        [Fact]
        public async Task GetProductHandler_Handle_ReturnsProducts()
        {
            var productServiceMock = new Mock<IProductService>();
            var request = new ProductQuery();
            var expectedProducts = new List<Product>();

            productServiceMock.Setup(x => x.GetProducts()).Returns(expectedProducts);

            var handler = new ProductQueryHandler(productServiceMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeEquivalentTo(expectedProducts);
        }

        [Fact]
        public async Task GetProductByColorHandler_Handle_ReturnsProducts()
        {
            var productServiceMock = new Mock<IProductService>();
            var request = new ProductQueryByColor("Red");
            var expectedProducts = new List<Product>();

            productServiceMock.Setup(x => x.GetProductsByColor(request.Color)).Returns(expectedProducts);

            var handler = new ProductQueryByColorHandler(productServiceMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeEquivalentTo(expectedProducts);
        }
    }
}


