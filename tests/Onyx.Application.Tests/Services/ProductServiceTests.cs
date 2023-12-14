using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;
using Moq;
using Onyx.Application.Mappers;
using Onyx.Application.Services;
using Onyx.Data.Repository;
using Onyx.Domain.Entities;

namespace Onyx.Application.Tests.Services
{
    public  class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public ProductServiceTests()
        {
            var logger =  new Mock<ILogger<ProductService>>();
            _productRepositoryMock = new Mock<IProductRepository>();
          
            _sut = new ProductService(logger.Object,_productRepositoryMock.Object, new ProductEntitiesToProductMapper());
        }

        [Fact]
        public void ProductService_ShouldReturnEmptyCollectionWhenThereAreNoProducts()
        {
            var products = new List<ProductEntity>{};
            _productRepositoryMock.Setup(x => x.GetProducts()).Returns(products);
            
            var result = _sut.GetProducts();

            using (var scope = new AssertionScope())
            {
                result.Should().BeEmpty();
            }
        }

        [Fact]
        public void ProductService_ShouldReturnAllProducts()
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Product 1", Color ="Red" },
                new ProductEntity { Id = 2, Name = "Product 2", Color = "Blue" }
            };
            _productRepositoryMock.Setup(x => x.GetProducts()).Returns(products);

            var result = _sut.GetProducts();

            using (var scope = new AssertionScope())
            {
                result.Should().NotBeEmpty();
                result.Should().HaveCount(2);
                result.Should().BeEquivalentTo(products);
            }
        }

        [Theory]
        [InlineData("red")]
        public void ProductService_ShouldReturnEmptyCollectionWhenThereAreNoProductsByColor(string color)
        {
            var products = new List<ProductEntity> { };
            _productRepositoryMock.Setup(x => x.GetProductsByColor(color)).Returns(products);

            var result = _sut.GetProductsByColor(color);

            using (var scope = new AssertionScope())
            {
                result.Should().BeEmpty();
            }
        }

        [Theory]
        [InlineData("red",1)]
        [InlineData("blue",2)]
        public void ProductService_ShouldReturn_AllProductsByColor(string color, int count)
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Product 1", Color ="Red" },
                new ProductEntity { Id = 2, Name = "Product 2", Color = "Blue" },
                new ProductEntity { Id = 3, Name = "Product 3", Color = "Blue" }
            };
          var expectedProducts = products.Where(x => x.Color.Equals(color, StringComparison.OrdinalIgnoreCase)).ToList();
            _productRepositoryMock.Setup(x=>x.GetProductsByColor(It.IsAny<string>())).Returns(
                expectedProducts.Select(x=>new ProductEntity(){Id=x.Id,Name = x.Name,Color = x.Name}).ToList());


            _productRepositoryMock.Setup(x => x.GetProductsByColor(color))
                .Returns(expectedProducts);


            var result = _sut.GetProductsByColor(color);


            using (var scope = new AssertionScope())
            {
                result.Count.Should().Be(count);
                result.Should().BeEquivalentTo(expectedProducts);
            }
        }
    }
}
