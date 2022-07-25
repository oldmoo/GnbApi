namespace GnbApi.Application.Common.Entities;

public abstract record Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
    public DateTime DateModified { get; init; } = DateTime.UtcNow;
}
