using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onyx.Data.DBContexts;
using Onyx.Data.Repository;

namespace Onyx.Data.Extensions
{
    public static class ServiceRegistrationsDataX
    {
        public static void RegisterService(IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(options =>
            {
                options.UseInMemoryDatabase("ProductsDB");
            });
            
            services.AddScoped<IProductRepository, ProductRepository>();

        }

    }
}
