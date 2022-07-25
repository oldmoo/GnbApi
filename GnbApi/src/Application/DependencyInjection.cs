namespace GnbApi.Application;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Common.Behaviours;
using FluentValidation;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.RateServices;
using Services.TransactionServices;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        _ = services.AddMediatR(Assembly.GetExecutingAssembly());
        _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        _ = services.AddSingleton<IRateServices, RaterService>();
        _ = services.AddSingleton<ITransactionServices, TransactionService>();
        return services;
    }
}
