namespace AggregationCodesPrinter;

/// <summary>
/// Настройки приложения
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    public string DbConnectionString { get; set; } = "Server=127.0.0.1;Database=warehouses_db;port=3306;User Id=root;password=root; convert zero datetime=True; Connection Timeout=43200;SSL Mode=None;";
    
    /// <summary>
    /// Путь к JSON файлу для сериализации/десериализации переменных этикетки
    /// </summary>
    public string VariablesJsonFilePath { get; set; } = "Variables.json";

    /// <summary>
    /// Путь к JSON файлу для сериализации/десериализации дескрипторов принтеров
    /// </summary>
    public string PrintersJsonFilePath { get; set; } = "Printers.json";
}