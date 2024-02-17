using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.UnitOfWorks;
using Healthcare.Api.Repository.Context;
using Healthcare.Api.Repository.Repositories;
using Healthcare.Api.Repository.UnitOfWorks;
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
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<ISpecialityRepository, SpecialityRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();

            return services;
        }

        private static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
