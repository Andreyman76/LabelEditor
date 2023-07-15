namespace AggregationCodesPrinter;

public static class ApplicationSettings
{
    public static string DbConnectionString { get; set; } = "Server=127.0.0.1;Database=warehouses_db;port=3306;User Id=root;password=root; convert zero datetime=True; Connection Timeout=43200;SSL Mode=None;";
    public static string Gln { get; set; } = "1234567890";
}