using FluentValidation;
using MediatR;
using MediatRPresentation.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRPresentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            //configuration.AddBehavior(typeof(ValidationBehaviors<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviors<,>));

        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        return services;
    }
}
