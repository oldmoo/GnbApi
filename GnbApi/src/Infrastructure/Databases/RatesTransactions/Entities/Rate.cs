namespace GnbApi.Infrastructure.Databases.RatesTransactions.Entities;

using Application.Common.Enums;


internal record Rates : Entity
{
    public Currency From { get; init; }
    public Currency To { get; init; }
    public decimal Rate { get; init; }
}
