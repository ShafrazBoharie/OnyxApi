using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Moq;
using Onyx.Data.DBContexts;
using Onyx.Data.Repository;
using Onyx.Domain.Entities;
using Xunit;


namespace Onyx.Data.Tests.Repository;

public class ProductContextTests
{
    private readonly Mock<DbSet<ProductEntity>> _mockSet;
    private readonly DbContextOptions<ProductContext> _options;

    public ProductContextTests()
    {
        _options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _mockSet = new Mock<DbSet<ProductEntity>>();
        _mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.Provider).Returns(AllProducts().AsQueryable().Provider);
        _mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.Expression)
            .Returns(AllProducts().AsQueryable().Expression);
        _mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.ElementType)
            .Returns(AllProducts().AsQueryable().ElementType);
        _mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.GetEnumerator())
            .Returns(() => AllProducts().GetEnumerator());
        _mockSet.As<IQueryable<ProductEntity>>().Setup(m => m.GetEnumerator())
            .Returns(() => AllProducts().GetEnumerator());
    }

    [Fact]
    public void GetProducts_ShouldReturn_CompleteProductList()
    {
        using (var context = new ProductContext(_options))
        {
            context.Products = _mockSet.Object;
            var service = new ProductRepository(context);

            using (new AssertionScope())
            {
                var products = service.GetProducts();
                products.Count().Should().Be(12);
                products.Should().BeEquivalentTo(AllProducts());
            }
        }
    }

    [Theory]
    [InlineData("Red", 1)]
    [InlineData("White", 4)]
    public void GetProductsByColor_ShouldReturn_ProductsFortheGiovenColor(string color, int count)
    {
        using (var context = new ProductContext(_options))
        {
            context.Products = _mockSet.Object;
            var service = new ProductRepository(context);

            var expectedProducts = ProductByColor(color);

            var products = service.GetProductsByColor(color);

            using (new AssertionScope())
            {
                products.Count().Should().Be(count);
                products.Should().BeEquivalentTo(expectedProducts);
            }
        }
    }

    private List<ProductEntity> ProductByColor(string color)
    {
        return AllProducts().Where(x => x.Color.Equals(color, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    private List<ProductEntity> AllProducts()
    {
        return new List<ProductEntity>
        {
            new() { Id = 1, Name = "Product A", Color = "Red" },
            new() { Id = 2, Name = "Product B", Color = "Blue" },
            new() { Id = 3, Name = "Product C", Color = "White" },
            new() { Id = 4, Name = "Product D", Color = "Blue" },
            new() { Id = 5, Name = "Product E", Color = "Violet" },
            new() { Id = 6, Name = "Product F", Color = "Yellow" },
            new() { Id = 7, Name = "Product G", Color = "White" },
            new() { Id = 8, Name = "Product H", Color = "White" },
            new() { Id = 9, Name = "Product I", Color = "Black" },
            new() { Id = 10, Name = "Product I", Color = "Blue" },
            new() { Id = 11, Name = "Product I", Color = "White" },
            new() { Id = 12, Name = "Product I", Color = "Green" }
        };
    }
}