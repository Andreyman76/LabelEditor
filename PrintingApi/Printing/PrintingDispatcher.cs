using LabelApi;
using MySqlDbApi;

namespace PrintingApi;

/// <summary>
/// Диспетчер печати
/// </summary>
public class PrintingDispatcher
{
    private readonly IPrintingDataSource _dataSource;
    private readonly List<IPrinterDescription> _printers = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataSource">Источник данных для печати</param>
    public PrintingDispatcher(IPrintingDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    /// <summary>
    /// Добавить принтер
    /// </summary>
    /// <param name="printer">Дескриптор принтера</param>
    public void AddPrinter(IPrinterDescription printer)
    {
        _printers.Add(printer);
    }

    /// <summary>
    /// Удалить принтер
    /// </summary>
    /// <param name="printer">Дескриптор принтера</param>
    public void RemovePrinter(IPrinterDescription printer)
    {
        _printers.Remove(printer);
    }

    /// <summary>
    /// Запустить задачи на всех принтерах
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void RunAllPrinters()
    {
        foreach(var printerDescription in _printers)
        {
            if(printerDescription.CurrentTask == null)
            {
                continue;
            }

            using var printer = printerDescription.CreatePrinter();
            
            foreach(var targetObjects in _dataSource.GetLabelDataObjects(printerDescription.CurrentTask.Key, printerDescription.CurrentTask.Count))
            {
                var label = printerDescription.CurrentTask.LabelTemplate.Clone() as PrinterLabel ?? throw new Exception("Cloning PrinterLabel failed");
                label.BindVariables(printerDescription.CurrentTask.LabelVariables, targetObjects);

                if (printer.Print(label))
                {
                    _dataSource.OnSuccessPrint(targetObjects);
                }
            }

            printerDescription.CurrentTask = null;
        }

        _printers.Clear();
    }
}