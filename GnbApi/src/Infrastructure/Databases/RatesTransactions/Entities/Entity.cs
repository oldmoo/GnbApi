namespace GnbApi.Infrastructure.Databases.RatesTransactions.Entities;

internal abstract record Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
}
