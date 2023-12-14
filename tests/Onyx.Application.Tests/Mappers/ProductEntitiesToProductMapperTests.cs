using FluentAssertions;
using FluentAssertions.Execution;
using Onyx.Application.Mappers;
using Onyx.Domain.Entities;

namespace Onyx.Application.Tests.Mappers
{
    public class ProductEntitiesToProductMapperTests
    {
        [Fact]
        public void SingleProductEntity_Should_Map_To_Product()
        {
            var productEntities = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Product 1", Color = "Red", },
            };

            var productEntitiesToProductMapper = new ProductEntitiesToProductMapper();

            var products = productEntitiesToProductMapper.Map(productEntities);

            using (new AssertionScope())
            {
                products.Should().NotBeNull();
                products.Should().HaveCount(1);
                products[0].Id.Should().Be(1);
                products[0].Name.Should().Be("Product 1");
                products[0].Color.Should().Be("Red");
            }
        }

        [Fact]
        public void MultipleProductEntities_Should_Map_To_Products()
        {
            var productEntities = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Product 1", Color = "Red", },
                new ProductEntity { Id = 2, Name = "Product 2", Color = "Blue", },
                new ProductEntity { Id = 3, Name = "Product 3", Color = "Green", },
            };

            var productEntitiesToProductMapper = new ProductEntitiesToProductMapper();

            var products = productEntitiesToProductMapper.Map(productEntities);

            using (new AssertionScope())
            {
                products.Should().NotBeNull();
                products.Should().HaveCount(3);
                products[0].Id.Should().Be(1);
                products[0].Name.Should().Be("Product 1");
                products[0].Color.Should().Be("Red");
                products[1].Id.Should().Be(2);
                products[1].Name.Should().Be("Product 2");
                products[1].Color.Should().Be("Blue");
                products[2].Id.Should().Be(3);
                products[2].Name.Should().Be("Product 3");
                products[2].Color.Should().Be("Green");
            }
        }   
    }
}
