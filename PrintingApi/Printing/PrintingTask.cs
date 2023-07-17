using LabelApi;

namespace PrintingApi;

/// <summary>
/// Задача принтера
/// </summary>
public class PrintingTask
{
    /// <summary>
    /// Дескриптор принтера
    /// </summary>
    public IPrinterDescription Printer { get; set; }

    /// <summary>
    /// Шаблон этикетки
    /// </summary>
    public PrinterLabel LabelTemplate { get; set; }

    /// <summary>
    /// Переменные этикетки
    /// </summary>
    public IEnumerable<LabelVariable> LabelVariables { get; set; }

    /// <summary>
    /// Ключевой объект для загрузки данных этикетки
    /// </summary>
    public object? Key { get; set; }

    /// <summary>
    /// Количество этикеток для печати
    /// </summary>
    public int Count { get; set; }
}