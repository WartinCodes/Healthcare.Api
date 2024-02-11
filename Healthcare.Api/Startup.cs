using Healthcare.Api.Repository;
using Healthcare.Api.Repository.Context;
using Healthcare.Api.Service;
using Helthcare.Api.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddService(this.Configuration);
            services.AddMappers();
            services.AddMvc();

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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://ecommerce-net.azurewebsites.net/",
                    ValidAudience = "https://ecommerce-net.azurewebsites.net/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta")),
                    ClockSkew = TimeSpan.Zero
                };
            });
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
            app.UseAuthentication();

            app.UseSwaggerConfig();

            app.UseReDocConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}