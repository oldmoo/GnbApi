namespace GnbApi.Infrastructure.Databases.RatesTransactions;

using System.Reflection;
using Entities;
using Microsoft.EntityFrameworkCore;

internal class GnbDbContext : DbContext
{
    public GnbDbContext(DbContextOptions<GnbDbContext> options) : base(options)
    {

    }

    public DbSet<Rates> Rate { get; set; }
    public DbSet<Transaction> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
