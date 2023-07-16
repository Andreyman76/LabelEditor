using MySqlDbApi;

namespace AggregationCodesPrinter;

public partial class DataSourceSelectionForm : Form
{
    private ILabelDataSource _dataSource;
    private List<object> _dataSourceList = new();
    public object? SelectedObject { get; private set; } = null;

    public DataSourceSelectionForm(ILabelDataSource dataSource)
    {
        InitializeComponent();
        _dataSource = dataSource;
        var first = true;

        foreach(var data in dataSource.GetKeyObjects())
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

    private void OnSelectButtonClick(object sender, EventArgs e)
    {
        var index = dataSourceGridView.SelectedRows[0].Index;

        SelectedObject = _dataSourceList[index];
        
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
}