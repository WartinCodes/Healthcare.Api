using Healthcare.Api.Repository.UnitOfWorks;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Repository.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Healthcare.Api.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IHealthcareDbContext>(provider => provider.GetService<HealthcareDbContext>());
            services.AddReppositories();

            services.AddUnitOfWorks();

            return services;
        }

        private static IServiceCollection AddReppositories(this IServiceCollection services)
        {
            //services.AddTransient<ICategoryRepository, CategoryRepository>();
            //services.AddTransient<IBrandRepository, BrandRepository>();
            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            //services.AddTransient<IFamilyRepository, FamilyRepository>();

            return services;
        }

        private static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
