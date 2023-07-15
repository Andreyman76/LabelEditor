using PrintingApi;
using LabelEditorApi;

namespace AggregationCodesPrinter;

public partial class PrintersEditorForm : Form
{
    private LabelEditor _editor;
    private PrintDialog _printDialog = new();

    public PrintersEditorForm(LabelEditor editor)
    {
        InitializeComponent();
        _editor = editor;
        UpdatePrintersList();
    }

    private void UpdatePrintersList()
    {
        printersListBox.Items.Clear();

        foreach (var name in _editor.Printers.Select(x => x.Name))
        {
            printersListBox.Items.Add(name);
        }
    }

    private void OnAddUsbPrinterButtonClick(object sender, EventArgs e)
    {
        var result = _printDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            var name = _printDialog.PrinterSettings.PrinterName;

            var lastIndex = _printDialog.PrinterSettings.PrinterResolutions.Count - 1;
            var dpiX = _printDialog.PrinterSettings.PrinterResolutions[lastIndex].X;
            var dpiY = _printDialog.PrinterSettings.PrinterResolutions[lastIndex].Y;

            _editor.AddPrinter(new UsbPrinterDescription()
            {
                Name = name,
                PrinterName = name,
                Dpi = new(dpiX, dpiY)
            });
        }

        UpdatePrintersList();
        printersListBox.SelectedIndex = printersListBox.Items.Count - 1;
    }

    private void OnAddNetPrinterButtonClick(object sender, EventArgs e)
    {
        _editor.AddPrinter(new NetPrinterDescription());
        UpdatePrintersList();
        printerPropertyGrid.SelectedObject = _editor.Printers.Last();
        printersListBox.SelectedIndex = printersListBox.Items.Count - 1;
    }

    private void OnDeleteButtonClick(object sender, EventArgs e)
    {
        if (printersListBox.SelectedIndex >= 0)
        {
            var printer = _editor.Printers[printersListBox.SelectedIndex];
            _editor.RemovePrinter(printer);

            printerPropertyGrid.SelectedObject = null;
            UpdatePrintersList();
        }
    }

    private void OnPrintersListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (printersListBox.SelectedIndex >= 0)
        {
            printerPropertyGrid.SelectedObject = _editor.Printers[printersListBox.SelectedIndex];
        }
    }

    private void OnPrinterPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        UpdatePrintersList();
    }

    private void OnPrintersEditorFormFormClosing(object sender, FormClosingEventArgs e)
    {
       _editor.SavePrintersToJson();
    }
}
