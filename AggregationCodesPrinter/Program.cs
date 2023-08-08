using MySqlDbApi;
using System.Text.Json;

namespace AggregationCodesPrinter;

internal static class Program
{
    /// <summary>
    /// ��������� ����������
    /// </summary>
    public static ApplicationSettings Settings { get; set; } = new ApplicationSettings();

    [STAThread]
    static void Main()
    {
        #region ������ �������� ���������� �� �����

        var settingsPath = "ApplicationSettings.json";

        if (File.Exists(settingsPath))
        {
            var json = File.ReadAllText(settingsPath);
            Settings = JsonSerializer.Deserialize<ApplicationSettings>(json) ?? throw new Exception($"Deserialization of {nameof(ApplicationSettings)} failed");
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

        // �������� ������ ��� ��������
        // ��� ��������� ���������� ������� ���� �����, ����������� ��������� IPrintingDataSource
        var dataSource = new GeneralDbStorage(Settings.DbConnectionString);

        var form = new LabelEditorForm(dataSource)
        {
            WindowState = FormWindowState.Maximized
        };

        Application.Run(form);
    }
}