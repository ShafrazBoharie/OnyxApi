using Onyx.Data.DBContexts;
using Onyx.Domain.Entities;

namespace Onyx.Data.Repository;

public class ProductRepository : IProductRepository, IDisposable
{
    private readonly ProductContext _context;

    public ProductRepository(ProductContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public List<ProductEntity> GetProducts()
    {
        return _context.Products.ToList();
    }

    public List<ProductEntity> GetProductsByColor(string color)
    {
        return _context.Products.Where(x => x.Color.Equals(color, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}