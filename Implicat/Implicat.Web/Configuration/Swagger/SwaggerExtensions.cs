using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Implicat.Configuration.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Implicat Api", Version = "v1" }); });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddOptions<SwaggerUIOptions>()
                .Configure(x => x.EnableDeepLinking());

            return services;
        }

        public static IApplicationBuilder UseSwaggerPipeline(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseSwagger()
                .UseSwaggerUI();

            return applicationBuilder;
        }
    }
}