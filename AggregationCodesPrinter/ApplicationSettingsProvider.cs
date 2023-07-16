namespace AggregationCodesPrinter;

/// <summary>
/// Класс, обеспечивающий глобальный доступ к настройкам приложения
/// </summary>
public static class ApplicationSettingsProvider
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public static ApplicationSettings Settings { get; set; } = new ApplicationSettings();
}