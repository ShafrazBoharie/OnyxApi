using MediatR;
using Onyx.Application.Mediator.Queries;
using Onyx.Application.Services;
using Onyx.Models.Models;

namespace Onyx.Application.Mediator
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery, List<Product>>
    {
        private readonly IProductService _productService;

        public ProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<Product>> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            return _productService.GetProducts();
        }
    }
}


