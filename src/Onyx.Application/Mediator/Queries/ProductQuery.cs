using MediatR;
using Onyx.Models.Models;

namespace Onyx.Application.Mediator.Queries
{
    public class ProductQuery : IRequest<List<Product>>
    {
    }
}
