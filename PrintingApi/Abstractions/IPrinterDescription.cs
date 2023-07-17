using LabelApi;

namespace PrintingApi;

/// <summary>
/// Дескриптор принтера
/// </summary>
public interface IPrinterDescription
{
    /// <summary>
    /// Отображаемое имя принтера
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Текущее задание принтера
    /// </summary>
    PrintingTask? CurrentTask { get; set; }

    /// <summary>
    /// Разрешение печати принтера
    /// </summary>
    Dpi Dpi { get; set; }

    /// <summary>
    /// Получить дескриптор принтера для сериализации
    /// </summary>
    /// <returns></returns>
    SerialaziblePrinterDescription GetPrinterDescription();

    /// <summary>
    /// Создать экземляр принтера на основании дескриптора
    /// </summary>
    /// <returns></returns>
    IPrinter CreatePrinter();

    /// <summary>
    /// Получить строку, содержащую имя принтера и описание текущей задачи
    /// </summary>
    /// <returns>Строка, содержащая имя принтера и описание текущей задачи</returns>
    string GetNameAndTask();
}