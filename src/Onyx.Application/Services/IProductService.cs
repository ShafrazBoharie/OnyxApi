using Onyx.Models.Models;

namespace Onyx.Application.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        List<Product> GetProductsByColor(string color);
    }
}
