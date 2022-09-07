using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using Implicat.Application.Extensions;
using Implicat.Application.Infrastructure;
using Implicat.Configuration;
using Implicat.Configuration.Hangfire;
using Implicat.Configuration.Options;
using Implicat.Configuration.Swagger;
using Implicat.Mapping;
using Implicat.Models.UserTasks.Validators;
using Microsoft.AspNetCore.Rewrite;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Implicat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(x => { x.Filters.Add<ModelStateValidationActionFilter>(); })
                .AddNewtonsoftJson(
                    x =>
                    {
                        x.SerializerSettings.Converters.Add(new StringEnumConverter());
                        x.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    })
                .AddControllersAsServices();

            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<GetUserTasksRequestValidator>();

            services.AddAutoMapper(typeof(UserTasksMappingProfile));
            ConfigureInfrastructure(services);
            services.AddApplicationLayer();

            services.Configure<BackgroundJobsOptions>(Configuration.GetSection(BackgroundJobsOptions.SectionName));

            services.AddHangfire();
            services.AddCustomCors();
            if (HostEnvironment.IsNotProduction)
            {
                services.AddSwagger();
            }
        }

        public void Configure(
            IApplicationBuilder app,
            IHostApplicationLifetime appLifetime,
            AutoMapper.IConfigurationProvider autoMapper)
        {
            app.UseRouting()
                .UseEndpoints(options => { options.MapControllers(); });

            if (HostEnvironment.IsNotProduction)
            {
                app.UseSwaggerPipeline();
            }

            autoMapper.AssertConfigurationIsValid();

            app.UseHangfire();
            app.UseCustomCors();

            app.UseRewriter(
                new RewriteOptions()
                    .AddRedirect(@"^$", "swagger", (int)HttpStatusCode.Redirect));
        }

        private void ConfigureInfrastructure(IServiceCollection services)
        {
            services.AddTransient<IClock, Clock>();
        }
    }
}