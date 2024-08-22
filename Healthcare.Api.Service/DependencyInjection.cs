using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Helper;
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
            services.AddTransient<IDoctorHealthInsuranceService, DoctorHealthInsuranceService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPatientHealthPlanService, PatientHealthPlanService>();
            services.AddTransient<IHealthInsuranceService, HealthInsuranceService>();
            services.AddTransient<IHealthPlanService, HealthPlanService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ISpecialityService, SpecialityService>();
            services.AddTransient<ILaboratoryDetailService, LaboratoryDetailService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IStudyTypeService, StudyTypeService>();
            services.AddTransient<IStudyService, StudyService>();
            services.AddTransient<ISupportService, SupportService>();
            services.AddTransient<IPatientHistoryService, PatientHistoryService>();

            services.AddTransient<IFileHelper, FileHelper>();

            services.AddTransient<IJwtService>(provider => new JwtService(configuration));

            services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
            services.AddTransient<IEmailService, EmailService>();

            services.Configure<TemplateConfiguration>(configuration.GetSection(nameof(TemplateConfiguration)));

            services.AddS3Services(configuration);

            return services;
        }

        private static IServiceCollection AddS3Services(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<S3Configuration>(configuration.GetSection(nameof(S3Configuration)));

            var s3Configuration = new S3Configuration();
            configuration.GetSection("S3Configuration").Bind(s3Configuration);

            string awsAccessKey = Environment.GetEnvironmentVariable("S3Configuration.AwsAccessKey") ?? s3Configuration.AwsAccessKey;
            string awsSecretAccessKey = Environment.GetEnvironmentVariable("S3Configuration.AwsSecretAccessKey") ?? s3Configuration.AwsSecretAccessKey;

            RegionEndpoint region = RegionEndpoint.GetBySystemName(s3Configuration.AwsRegion);
            services.AddSingleton<IAmazonS3>(new AmazonS3Client(awsAccessKey, awsSecretAccessKey, region));
            services.AddTransient<ITransferUtility, TransferUtility>();

            return services;
        }

    }
}
