namespace LabelEditor;

public partial class DataSourceSelectionForm : Form
{
    public DataSourceSelectionForm()
    {
        InitializeComponent();
    }

    private void OnSelectButtonClick(object sender, EventArgs e)
    {
        //TODO: See printers!
        DialogResult = DialogResult.OK;
        Close();
    }

    private void OnDataSourceSelectionFormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult=DialogResult.Cancel;
    }
}