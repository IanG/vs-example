using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VsExample.Application.PipelineBehaviors;

namespace VsExample.Application;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Add MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            // Add query and command lifetime logging
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(QueryLoggingBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CommandLoggingBehaviour<,>));
        });
        
        // Add Fluent Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}