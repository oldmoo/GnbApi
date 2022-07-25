namespace GnbApi.Infrastructure.Databases.RatesTransactions.Entities;

using Application.Common.Enums;

internal record Transaction : Entity
{
    public string Sku { get; init; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}
