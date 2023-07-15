using MySqlDbApi;

namespace AggregationCodesPrinter
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var labelDataSource = new MySqlStorage2(ApplicationSettings.DbConnectionString);

            Application.Run(new LabelEditorForm(labelDataSource));
        }
    }
}