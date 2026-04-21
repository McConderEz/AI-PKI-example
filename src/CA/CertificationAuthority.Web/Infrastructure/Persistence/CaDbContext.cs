using CertificationAuthority.Web.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CertificationAuthority.Web.Infrastructure.Persistence;

/// <summary>
/// DbContext центра сертификации.
/// </summary>
public sealed class CaDbContext : DbContext
{
    /// <summary>
    /// Инициализирует контекст данных CA.
    /// </summary>
    public CaDbContext(DbContextOptions<CaDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Сертификаты.
    /// </summary>
    public DbSet<Certificate> Certificates => Set<Certificate>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
