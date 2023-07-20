using LabelApi;

namespace PrintingApi;

/// <summary>
/// Задача принтера
/// </summary>
public class PrinterTask
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="printer">Дескриптор принтера</param>
    /// <param name="labelTemplate">Шаблон этикетки</param>
    /// <param name="labelVariables">Переменные этикетки</param>
    /// <param name="key">Ключевой объект для загрузки данных этикетки</param>
    /// <param name="count">Количество этикеток для печати</param>
    public PrinterTask(
        IPrinterDescription printer, 
        PrinterLabel labelTemplate, 
        IEnumerable<LabelVariable> labelVariables, 
        object? key, 
        int count)
    { 
        Printer = printer;
        LabelTemplate = labelTemplate;
        LabelVariables = labelVariables;
        Key = key;
        Count = count;
    }
}