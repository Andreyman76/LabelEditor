using MySqlDbApi;
using System.Text.Json;

namespace AggregationCodesPrinter
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            #region Чтение настроек приложения из файла
            var settingsPath = "ApplicationSettings.json";

            if (File.Exists(settingsPath))
            {
                var json = File.ReadAllText(settingsPath);
                ApplicationSettingsProvider.Settings = JsonSerializer.Deserialize<ApplicationSettings>(json) ?? throw new Exception($"Deserialization of {nameof(ApplicationSettings)} failed");
            }
            else
            {
                var json = JsonSerializer.Serialize(new ApplicationSettings(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(settingsPath, json);
            }

            #endregion

            ApplicationConfiguration.Initialize();



            // Источник данных для этикеток
            // Для изменения необходимо создать свой класс, реализующий интерфейс ILabelDataSource
            var labelDataSource = new MySqlStorage2(ApplicationSettingsProvider.Settings.DbConnectionString);

            Application.Run(new LabelEditorForm(labelDataSource));
        }
    }
}