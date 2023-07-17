using LabelApi;
using MySqlDbApi;
using System.Collections.ObjectModel;

namespace PrintingApi;

/// <summary>
/// Диспетчер печати
/// </summary>
public class PrintingDispatcher
{
    public ReadOnlyCollection<PrinterTask> Tasks { get => _tasks.AsReadOnly(); }

    private readonly IPrintingDataSource _dataSource;
    private readonly List<PrinterTask> _tasks = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataSource">Источник данных для печати</param>
    public PrintingDispatcher(IPrintingDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    /// <summary>
    /// Добавить задачу
    /// </summary>
    /// <param name="task">Задача принтера</param>
    public void AddTask(PrinterTask task)
    {
        _tasks.Add(task);
    }

    /// <summary>
    /// Удалить задачу
    /// </summary>
    /// <param name="task">Задача принтера</param>
    public void RemoveTask(PrinterTask task)
    {
        _tasks.Remove(task);
    }

    /// <summary>
    /// Запустить все задачи
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void RunAllPrinters()
    {
        foreach(var task in _tasks)
        {
            using var printer = task.Printer.CreatePrinter();
            
            foreach(var targetObjects in _dataSource.GetLabelDataObjects(task.Key, task.Count))
            {
                var label = task.LabelTemplate.Clone() as PrinterLabel ?? throw new Exception("Cloning PrinterLabel failed");
                label.BindVariables(task.LabelVariables, targetObjects);

                _dataSource.AfterPrint(targetObjects, printer.Print(label));
                
            }
        }

        _tasks.Clear();
    }
}