using Microsoft.Extensions.DependencyInjection;
using Onyx.Application.Mappers;
using Onyx.Application.Mediator;
using Onyx.Application.Services;
using Onyx.Data.Extensions;

namespace Onyx.Application.Extensions
{
    public static class ServiceRegistrationAppX
    {
        public static void RegisterService(IServiceCollection services)
        {
            ServiceRegistrationsDataX.RegisterService(services);

            services.AddTransient<ProductEntitiesToProductMapper>();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(ProductQueryHandler)));
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(ProductQueryByColorHandler)));

            services.AddScoped<IProductService, ProductService>();

        }

    }
}
