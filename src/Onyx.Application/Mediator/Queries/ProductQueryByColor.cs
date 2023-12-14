using MediatR;
using Onyx.Models.Models;

namespace Onyx.Application.Mediator.Queries
{
    public class ProductQueryByColor : IRequest<List<Product>>
    {
        public string Color { get; set; }

        public ProductQueryByColor(string color)
        {
            Color = color;
        }
    }
}
