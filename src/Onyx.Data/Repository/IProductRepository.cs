using Onyx.Domain.Entities;

namespace Onyx.Data.Repository;

public interface IProductRepository
{
    List<ProductEntity> GetProducts();
    List<ProductEntity> GetProductsByColor(string color);

    // Conventional Crud operations avoided to protect from YAGNI violation in this context
}