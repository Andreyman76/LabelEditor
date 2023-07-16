namespace MySqlDbApi;

public interface ILabelDataSource
{
    /// <summary>
    /// Тестовые объекты, отображаемые в редакторе
    /// </summary>
    IEnumerable<object> TestObjects { get; }

    /// <summary>
    /// Получить список ключевых объектов, на основании которых делается последующая выборка
    /// </summary>
    /// <returns></returns>
    IEnumerable<object> GetKeyObjects();

    /// <summary>
    /// Получить список объектов для этикетки на основании ключевого объекта
    /// </summary>
    /// <param name="key">Ключевой объект</param>
    /// <param name="count">Количество</param>
    /// <returns></returns>
    IEnumerable<object[]> GetLabelDataObjects(object? key, int count);

    /// <summary>
    /// Действия при успешной печати этикетки
    /// </summary>
    /// <param name="printedObjects">Список объектов, использованных при печати</param>
    void OnSuccessPrint(object[] printedObjects);
}