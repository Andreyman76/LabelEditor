namespace MySqlDbApi;

/// <summary>
/// Источник данных для этикетки
/// </summary>
public interface IPrintingDataSource
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
    /// Действия после печати этикетки
    /// </summary>
    /// <param name="printedObjects">Список объектов, использованных при печати</param>
    /// <param name="isSuccess">Была ли печать успешной</param>
    void AfterPrint(object[] printedObjects, bool isSuccess);
}