namespace AggregationCodesPrinter;

public class ApplicationSettings
{
    public string DbConnectionString { get; set; } = "Server=127.0.0.1;Database=warehouses_db;port=3306;User Id=root;password=root; convert zero datetime=True; Connection Timeout=43200;SSL Mode=None;";
    public string VariablesJsonFilePath { get; set; } = "Variables.json";
    public string PrintersJsonFilePath { get; set; } = "Printers.json";
}