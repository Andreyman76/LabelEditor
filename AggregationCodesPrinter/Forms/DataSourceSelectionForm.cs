using MySqlDbApi;

namespace AggregationCodesPrinter;

public partial class DataSourceSelectionForm : Form
{
    private readonly List<object> _dataSourceList = new();
    private readonly IPrintingDataSource _dataSource;
    public object? SelectedObject { get; private set; } = null;

    public DataSourceSelectionForm(IPrintingDataSource dataSource)
    {
        _dataSource = dataSource;
        InitializeComponent();
    }



    private void OnSelectButtonClick(object sender, EventArgs e)
    {
        if (dataSourceGridView.SelectedRows.Count != 0)
        {
            var index = dataSourceGridView.SelectedRows[0].Index;

            SelectedObject = _dataSourceList[index];
        }

        Close();
        DialogResult = DialogResult.OK;
    }

    private void OnDataSourceSelectionFormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

    private void OnDataSourceSelectionFormShown(object sender, EventArgs e)
    {
        dataSourceGridView.Rows.Clear();
        dataSourceGridView.Columns.Clear();

        var first = true;

        foreach (var data in _dataSource.GetKeyObjects())
        {
            var properties = data.GetType().GetProperties();

            if (first)
            {
                foreach (var name in properties.Select(x => x.Name))
                {
                    dataSourceGridView.Columns.Add(name, name);
                }
            }

            dataSourceGridView.Rows.Add(properties.Select(x => x.GetValue(data)).ToArray());
            _dataSourceList.Add(data);
            first = false;
        }
    }
}