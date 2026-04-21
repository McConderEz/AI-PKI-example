namespace RegistrationAuthority.Web.Infrastructure.Repositories;

/// <summary>
/// Единица работы для фиксации изменений EF Core.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохраняет изменения.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
