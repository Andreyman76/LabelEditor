using MySqlDbApi;
using System.Text.Json;

namespace AggregationCodesPrinter;

internal static class Program
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public static ApplicationSettings Settings { get; set; } = new ApplicationSettings();

    [STAThread]
    static void Main()
    {
        #region Чтение настроек приложения из файла

        if (File.Exists(ApplicationSettings.SettingPath))
        {
            var json = File.ReadAllText(ApplicationSettings.SettingPath);
            Settings = JsonSerializer.Deserialize<ApplicationSettings>(json) ?? throw new Exception($"Deserialization of {nameof(ApplicationSettings)} failed");
        }
        else
        {
            var json = JsonSerializer.Serialize(new ApplicationSettings(), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(ApplicationSettings.SettingPath, json);
        }

        #endregion

        ApplicationConfiguration.Initialize();

        // Источник данных для этикеток
        // Для изменения необходимо создать свой класс, реализующий интерфейс IPrintingDataSource
        var dataSource = new VulkanicaStorage(Settings.DbConnectionString);

        var form = new LabelEditorForm(dataSource)
        {
            WindowState = FormWindowState.Maximized
        };

        Application.Run(form);
    }
}