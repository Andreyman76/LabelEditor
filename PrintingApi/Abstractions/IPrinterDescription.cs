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
    /// Разрешение печати принтера
    /// </summary>
    Dpi Dpi { get; set; }

    /// <summary>
    /// Получить дескриптор принтера для сериализации
    /// </summary>
    /// <returns></returns>
    SerialazablePrinterDescription GetPrinterDescription();

    /// <summary>
    /// Создать экземляр принтера на основании дескриптора
    /// </summary>
    /// <returns></returns>
    IPrinter CreatePrinter();
}