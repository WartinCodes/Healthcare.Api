﻿using Healthcare.Api.Core.RepositoryInterfaces;
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
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IBloodTestRepository, BloodTestRepository>();
            services.AddTransient<IBloodTestDataRepository, BloodTestDataRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IDoctorSpecialityRepository, DoctorSpecialityRepository>();
            services.AddTransient<IDoctorHealthInsuranceRepository, DoctorHealthInsuranceRepository>();
            services.AddTransient<IHealthInsuranceRepository, HealthInsuranceRepository>();
            services.AddTransient<IHealthPlanRepository, HealthPlanRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPatientHealthPlanRepository, PatientHealthPlanRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ISpecialityRepository, SpecialityRepository>();
            services.AddTransient<IStudyTypeRepository, StudyTypeRepository>();
            services.AddTransient<IStudyRepository, StudyRepository>();
            services.AddTransient<ISupportRepository, SupportRepository>();
            services.AddTransient<IPatientHistoryRepository, PatientHistoryRepository>();
            services.AddTransient<IUltrasoundImageRepository, UltrasoundImageRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<INutritionDataRepository, NutritionDataRepository>();

            return services;
        }

        private static IServiceCollection AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
