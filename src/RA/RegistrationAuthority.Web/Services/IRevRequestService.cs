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
    /// <param name="request">Запрос на создание заявки.</param>
    /// <returns>Созданная заявка или <c>null</c>, если пользователь не найден.</returns>
    RevRequest? Create(CreateRevRequest request);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <returns>Заявка или <c>null</c>, если не найдена.</returns>
    RevRequest? GetById(Guid id);

    /// <summary>
    /// Возвращает список всех заявок.
    /// </summary>
    /// <returns>Коллекция заявок.</returns>
    IReadOnlyCollection<RevRequest> GetAll();

    /// <summary>
    /// Возвращает заявки конкретного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Коллекция заявок пользователя.</returns>
    IReadOnlyCollection<RevRequest> GetByUser(Guid userId);
}
