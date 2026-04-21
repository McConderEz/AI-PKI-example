using Microsoft.EntityFrameworkCore;
using RegistrationAuthority.Web.Domain.Entities;

namespace RegistrationAuthority.Web.Infrastructure.Persistence;

/// <summary>
/// DbContext регистрационного центра.
/// </summary>
public sealed class RaDbContext : DbContext
{
    /// <summary>
    /// Инициализирует экземпляр контекста данных RA.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста.</param>
    public RaDbContext(DbContextOptions<RaDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Заявки на выпуск сертификатов.
    /// </summary>
    public DbSet<CertRequest> CertRequests => Set<CertRequest>();

    /// <summary>
    /// Сертификаты.
    /// </summary>
    public DbSet<Certificate> Certificates => Set<Certificate>();

    /// <summary>
    /// Заявки на отзыв сертификатов.
    /// </summary>
    public DbSet<RevRequest> RevRequests => Set<RevRequest>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
