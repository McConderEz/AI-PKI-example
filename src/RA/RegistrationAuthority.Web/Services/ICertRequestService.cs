using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления заявками на выпуск сертификатов.
/// </summary>
public interface ICertRequestService
{
    /// <summary>
    /// Создает заявку на выпуск сертификата.
    /// </summary>
    /// <param name="request">Запрос на создание заявки.</param>
    /// <returns>Созданная заявка или <c>null</c>, если пользователь не найден.</returns>
    CertRequest? Create(CreateCertRequest request);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <returns>Заявка или <c>null</c>, если не найдена.</returns>
    CertRequest? GetById(Guid id);

    /// <summary>
    /// Возвращает список всех заявок.
    /// </summary>
    /// <returns>Коллекция заявок.</returns>
    IReadOnlyCollection<CertRequest> GetAll();

    /// <summary>
    /// Возвращает заявки конкретного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Коллекция заявок пользователя.</returns>
    IReadOnlyCollection<CertRequest> GetByUser(Guid userId);

    /// <summary>
    /// Отмечает заявку как успешно обработанную и связывает ее с выпущенным сертификатом.
    /// </summary>
    /// <param name="certRequestId">Идентификатор заявки на выпуск.</param>
    /// <param name="certificateId">Идентификатор выпущенного сертификата.</param>
    /// <returns><c>true</c>, если заявка обновлена, иначе <c>false</c>.</returns>
    bool MarkIssued(Guid certRequestId, Guid certificateId);
}
