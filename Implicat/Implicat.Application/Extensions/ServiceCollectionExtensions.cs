using System.Reflection;
using Implicat.Application.Infrastructure.MediatorPipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Implicat.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        return services;
    }
}