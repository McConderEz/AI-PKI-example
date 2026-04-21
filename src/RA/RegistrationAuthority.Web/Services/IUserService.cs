using RegistrationAuthority.Web.Domain.Entities;
using RegistrationAuthority.Web.Domain.Requests;

namespace RegistrationAuthority.Web.Services;

/// <summary>
/// Сервис управления пользователями RA.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Создает пользователя.
    /// </summary>
    /// <param name="request">Запрос на создание пользователя.</param>
    /// <returns>Созданный пользователь.</returns>
    User Create(CreateUserRequest request);

    /// <summary>
    /// Обновляет существующего пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="request">Запрос на обновление.</param>
    /// <returns>Обновленный пользователь или <c>null</c>, если пользователь не найден.</returns>
    User? Update(Guid id, UpdateUserRequest request);

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Пользователь или <c>null</c>, если запись не найдена.</returns>
    User? GetById(Guid id);

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    /// <returns>Коллекция пользователей.</returns>
    IReadOnlyCollection<User> GetAll();
}
