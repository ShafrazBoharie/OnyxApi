using Microsoft.Extensions.Logging;
using Onyx.Application.Mappers;
using Onyx.Data.Repository;
using Onyx.Models.Models;

namespace Onyx.Application.Services
{
    public class ProductService :IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepo;
        private readonly ProductEntitiesToProductMapper _productMapper;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepo,ProductEntitiesToProductMapper productMapper)
        {
            _logger = logger;
            _productRepo= productRepo;
            _productMapper = productMapper;
        }
        public List<Product> GetProducts()
        {
            _logger.LogInformation("GetProducts called");
            var productEntities = _productRepo.GetProducts();
            return _productMapper.Map(productEntities);
        }

        public List<Product> GetProductsByColor(string color)
        {
            _logger.LogInformation("GetProductsByColor called");
            var productEntities = _productRepo.GetProductsByColor(color);
            return _productMapper.Map(productEntities);
        }
    }
}
