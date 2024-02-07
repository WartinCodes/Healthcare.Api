using Microsoft.Extensions.DependencyInjection;

namespace Healthcare.Api.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            //services.AddTransient<IBrandService, BrandService>();
            //services.AddTransient<ICategoryService, CategoryService>();
            //services.AddTransient<IProductService, ProductService>();
            //services.AddTransient<ISubCategoryService, SubCategoryService>();
            //services.AddTransient<IFamilyService, FamilyService>();

            return services;
        }
    }
}
