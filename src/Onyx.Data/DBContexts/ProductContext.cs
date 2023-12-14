using Microsoft.EntityFrameworkCore;
using Onyx.Domain.Entities;

namespace Onyx.Data.DBContexts;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options): base(options)
    {
        this.EnsureSeedData();
    }
    
    public DbSet<ProductEntity> Products { get; set; }

}