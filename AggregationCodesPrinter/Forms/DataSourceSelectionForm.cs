namespace AggregationCodesPrinter;

public partial class DataSourceSelectionForm : Form
{
    public DataSourceSelectionForm()
    {
        InitializeComponent();
    }

    private void OnSelectButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnDataSourceSelectionFormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}