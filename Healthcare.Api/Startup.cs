using Healthcare.Api.Repository;
using Healthcare.Api.Repository.Context;
using Healthcare.Api.Service;
using Helthcare.Api.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Helthcare.Api
{
    public class Startup
    {
        readonly string MyPolicy = "MyPolicy";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HealthcareDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(HealthcareDbContext)),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            services.SetupSwagger();

            services.AddRepository();
            services.AddService();
            services.AddMappers();

            services.AddCors(o => o.AddPolicy(MyPolicy, builder =>
            {
                if (Configuration.GetValue<bool>("isAllowAllCrossOrigins"))
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                }
                else
                {
                    builder.WithOrigins(Configuration.GetSection("AllowedOriginsList").GetChildren().ToArray().Select(c => c.Value).ToArray());
                }
            }));

            services.AddControllers(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyPolicy);

            app.UseAuthorization();

            app.UseSwaggerConfig();

            app.UseReDocConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}