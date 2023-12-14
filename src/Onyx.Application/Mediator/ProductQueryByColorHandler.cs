using MediatR;
using Onyx.Application.Mediator.Queries;
using Onyx.Application.Services;
using Onyx.Models.Models;

namespace Onyx.Application.Mediator
{
    public class ProductQueryByColorHandler : IRequestHandler<ProductQueryByColor, List<Product>>
    {
        private readonly IProductService _productService;

        public ProductQueryByColorHandler(IProductService productService)
        {
            _productService = productService;
           
        }
        public async Task<List<Product>> Handle(ProductQueryByColor request, CancellationToken cancellationToken)
        {
            return _productService.GetProductsByColor(request.Color);
        }
    }
}
