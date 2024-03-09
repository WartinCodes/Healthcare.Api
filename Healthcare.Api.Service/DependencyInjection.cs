using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Healthcare.Api.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IDoctorSpecialityService, DoctorSpecialityService>();
            services.AddTransient<IDoctorHealthPlanService, DoctorHealthPlanService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IHealthInsuranceService, HealthInsuranceService>();
            services.AddTransient<IHealthPlanService, HealthPlanService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ISpecialityService, SpecialityService>();

            services.AddTransient<IJwtService>(provider => new JwtService(configuration));

            return services;
        }
    }
}
