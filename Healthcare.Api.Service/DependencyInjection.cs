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
            services.AddTransient<ISpecialityService, SpecialityService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IHealthInsuranceService, HealthInsuranceService>();
            services.AddTransient<IHealthPlanService, HealthPlanService>();

            services.AddTransient<IJwtService>(provider => new JwtService(configuration));

            return services;
        }
    }
}
