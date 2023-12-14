using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Onyx.Api.Controllers;

namespace Onyx.Api.Tests.Controllers
{
    public class HealthControllerTests
    {
        [Fact]
        public void CheckHealth_ReturnsOk()
        {
            var logger = new Mock<ILogger<HealthController>>();
            var controller = new HealthController(logger.Object);

            var result = controller.CheckHealth();

            var okResult = Assert.IsType<OkObjectResult>(result);

            okResult.StatusCode.Should().Be(200);
           
        }
    }
}
