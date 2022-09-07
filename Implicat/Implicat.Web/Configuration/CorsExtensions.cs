using Implicat.Application.Infrastructure;

namespace Implicat.Configuration;

public static class CorsExtensions
{
    private const string CorsName = "LocalCorsName";

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        // On all environments cors managed by nginx
        if (HostEnvironment.IsLocal)
        {
            services.AddCors(
                o => o.AddPolicy(
                    CorsName,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Allow Client app to call backend 
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders("Content-Disposition");
                    }));
        }

        return services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseCors(CorsName);
    }
}