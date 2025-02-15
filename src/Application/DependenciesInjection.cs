using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependenciesInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        System.Reflection.Assembly assembly = typeof(DependenciesInjection).Assembly;
        return services
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly))
            .AddValidatorsFromAssembly(assembly);
    }
}
