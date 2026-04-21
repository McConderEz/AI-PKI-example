using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления заявками на отзыв сертификатов.
/// </summary>
public interface IRevRequestService
{
    /// <summary>
    /// Создает заявку на отзыв сертификата.
    /// </summary>
    Task<RevRequest?> CreateAsync(CreateRevRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет заявку на отзыв сертификата.
    /// </summary>
    Task<RevRequest?> UpdateAsync(Guid id, UpdateRevRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет заявку на отзыв.
    /// </summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    Task<RevRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает все заявки.
    /// </summary>
    Task<IReadOnlyCollection<RevRequest>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает заявки пользователя.
    /// </summary>
    Task<IReadOnlyCollection<RevRequest>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
