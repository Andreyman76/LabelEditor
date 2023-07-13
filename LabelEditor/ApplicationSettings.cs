namespace LabelEditor;

public class ApplicationSettings
{
    public string DbConnectionString { get; set; } = string.Empty;
    public string PrinterIp { get; set; } = string.Empty;
    public int PrinterPort { get; set; } = default;
    public string Gln { get; set; } = string.Empty;

}
