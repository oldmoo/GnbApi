namespace GnbApi.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Application.Rates;
using Application.Transactions;
using Databases.RatesTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleDateTimeProvider;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddEntityFrameworkInMemoryDatabase();
        _ = services.AddDbContext<GnbDbContext>(options => options.UseInMemoryDatabase($"Gnb-{Guid.NewGuid()}"), ServiceLifetime.Singleton);

        _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        _ = services.AddSingleton<EntityFrameworkRateTransactionRepository>();
        _ = services.AddSingleton<IRatesRepository>(p => p.GetRequiredService<EntityFrameworkRateTransactionRepository>());
        _ = services.AddSingleton<ITransactionRepository>(p => p.GetRequiredService<EntityFrameworkRateTransactionRepository>());

        _ = services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}
