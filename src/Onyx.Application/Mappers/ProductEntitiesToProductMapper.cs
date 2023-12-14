using Onyx.Domain.Entities;
using Onyx.Models.Models;

namespace Onyx.Application.Mappers
{
    public class ProductEntitiesToProductMapper
    {
        public virtual List<Product> Map(List<ProductEntity> productEntities)
        {
            return productEntities.Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
            }).ToList();
        }
    }
}
