using LabelApi;
using LabelEditorApi;
using MySqlDbApi;
using PrintingApi;

namespace AggregationCodesPrinter;

public partial class LabelEditorForm : Form
{
    private object? _selectedObject;
    private readonly IEnumerable<object> _testObjects;
    private readonly IPrintingDataSource _dataSource;
    private readonly DataSourceSelectionForm _dataSourceSelectionForm;
    private readonly VariablesEditorForm _variablesEditorForm;
    private readonly PrintersEditorForm _printersEditorForm;
    private readonly PrintingDispatcher _printingDispatcher;
    private readonly LabelEditor _editor = new();

    private readonly OpenFileDialog _openFileDialog = new()
    {
        DefaultExt = "xml",
        Filter = "XML Files|*.xml;"
    };

    private readonly SaveFileDialog _saveFileDialog = new()
    {
        DefaultExt = "xml",
        Filter = "XML Files|*.xml;"
    };

    public LabelEditorForm(IPrintingDataSource dataSource)
    {
        _dataSource = dataSource;
        _testObjects = dataSource.TestObjects;
        _printingDispatcher = new(dataSource);

        foreach (var testObject in _testObjects)
        {
            _editor.RegisterType(testObject.GetType());
        }

        _editor.LoadVariablesFromJson(Program.Settings.VariablesJsonFilePath);
        _editor.LoadPrintersFromJson(Program.Settings.PrintersJsonFilePath);

        _dataSourceSelectionForm = new(_dataSource);
        _variablesEditorForm = new(_editor);
        _printersEditorForm = new(_editor);

        InitializeComponent();

        labelPropertyGrid.SelectedObject = _editor.LabelTemplate;
        dataSourceGridView.Columns.Add("Prompt", "Источник данных");
        dataSourceGridView.Rows.Add("Не выбран");
        UpdateListOfPrinters();

        Redraw();
    }

    private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        _editor.LabelTemplate = new()
        {
            Size = new(22, 22)
        };

        labelPropertyGrid.SelectedObject = _editor.LabelTemplate;
        labelElementPropertyGrid.SelectedObject = null;

        UpdateListOfObjects();
        Redraw();
    }

    private void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
    {
        var result = _saveFileDialog.ShowDialog(this);

        if (result == DialogResult.OK)
        {
            _editor.SaveLabelToXml(_saveFileDialog.FileName);

        }
    }

    private void OnLoadToolStripMenuItemClick(object sender, EventArgs e)
    {
        var result = _openFileDialog.ShowDialog(this);

        if (result == DialogResult.OK)
        {
            _editor.LoadLabelFromXml(_openFileDialog.FileName);
            labelPropertyGrid.SelectedObject = _editor.LabelTemplate;
        }

        UpdateListOfObjects();
        Redraw();
    }

    private void OnPrintersToolStripMenuItemClick(object sender, EventArgs e)
    {
        _printersEditorForm.ShowDialog();

        UpdateListOfPrinters();
    }

    private void OnEditVariablesToolStripMenuItemClick(object sender, EventArgs e)
    {
        _variablesEditorForm.ShowDialog();
        Redraw();
    }

    private void OnDataSourceToolStripMenuItemClick(object sender, EventArgs e)
    {
        var result = _dataSourceSelectionForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            _selectedObject = _dataSourceSelectionForm.SelectedObject;

            if (_selectedObject != null)
            {
                dataSourceGridView.Columns.Clear();
                dataSourceGridView.Rows.Clear();

                var properties = _selectedObject.GetType().GetProperties();

                foreach (var name in properties.Select(x => x.Name))
                {
                    dataSourceGridView.Columns.Add(name, name);
                }

                dataSourceGridView.Rows.Add(properties.Select(x => x.GetValue(_selectedObject)).ToArray());
            }
        }
    }

    private void OnAddTextButtonClick(object sender, EventArgs e)
    {
        _editor.LabelTemplate.Elements.Add(
            new LabelText
            {
                Name = "Text",
                Text = "1234567890"
            }
            );

        UpdateListOfObjects();
        Redraw();
    }

    private void OnAddDmButtonClick(object sender, EventArgs e)
    {
        _editor.LabelTemplate.Elements.Add(
           new LabelDataMatrix
           {
               Name = "DataMatrix",
               Code = "${gs}0105449000203359215gHAvnw6TXwN4${gs}93dGVz",
               Size = 20
           }
           );

        UpdateListOfObjects();
        Redraw();
    }

    private void OnAddCode128ButtonClick(object sender, EventArgs e)
    {
        _editor.LabelTemplate.Elements.Add(
           new LabelCode128
           {
               Name = "Code128",
               Code = "0123456789",
               Size = new(20, 10)
           }
           );

        UpdateListOfObjects();
        Redraw();
    }

    private void OnAddImageButtonClick(object sender, EventArgs e)
    {
        var dialog = new OpenFileDialog();

        var result = dialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            var imageBytes = File.ReadAllBytes(dialog.FileName);

            _editor.LabelTemplate.Elements.Add(
            new LabelImage
            {
                Name = "Image",
                Size = new(20, 20),
                ImageBytes = imageBytes
            }
            );

            UpdateListOfObjects();
            Redraw();
        }
    }

    private void OnAddEllipseButtonClick(object sender, EventArgs e)
    {
        _editor.LabelTemplate.Elements.Add(
           new LabelEllipse
           {
               Name = "Ellipse",
               Size = new(20, 20)
           }
           );

        UpdateListOfObjects();
        Redraw();
    }

    private void OnDeleteElementButtonClick(object sender, EventArgs e)
    {
        if (labelElementsListBox.SelectedIndex >= 0)
        {
            labelElementPropertyGrid.SelectedObject = null;
            _editor.LabelTemplate.Elements.RemoveAt(labelElementsListBox.SelectedIndex);
        }

        UpdateListOfObjects();
        Redraw();
    }

    private void UpdateListOfObjects()
    {
        labelElementsListBox.Items.Clear();

        foreach (var element in _editor.LabelTemplate.Elements)
        {
            labelElementsListBox.Items.Add(element.Name);
        }
    }

    private void UpdateListOfPrinters()
    {
        printersListBox.Items.Clear();

        foreach (var printer in _editor.Printers)
        {
            var printerTitle = printer.Name;

            var task = _printingDispatcher.Tasks.FirstOrDefault(x => x.Printer == printer);

            if (task != null)
            {
                printerTitle += $" - Задача {task.Count} шт";
            }

            printersListBox.Items.Add(printerTitle);
        }
    }

    private void Redraw()
    {
        renderedLabelPictureBox.BackColor = Color.Gray;
        renderedLabelPictureBox.Image = _editor.GetCurrentLabel(_testObjects).GetImage(new(600, 600));
    }

    private void OnLabelElementsListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (labelElementsListBox.SelectedIndex >= 0)
        {
            labelElementPropertyGrid.SelectedObject = _editor.LabelTemplate.Elements[labelElementsListBox.SelectedIndex];
        }
    }

    private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        Redraw();
    }

    private void OnPrintButtonClick(object sender, EventArgs e)
    {
        _printingDispatcher.RunAllPrinters();
        UpdateListOfPrinters();
    }

    private void OnAddTaskButtonClick(object sender, EventArgs e)
    {
        if (printersListBox.SelectedIndex >= 0)
        {
            var printer = _editor.Printers[printersListBox.SelectedIndex];
            var template = _editor.LabelTemplate.Clone() as PrinterLabel ?? throw new Exception("Cloning PrinterLabel fail");
            
            var task = new PrinterTask(
                printer,
                template,
                _editor.Variables,
                _selectedObject,
                (int)numericUpDown1.Value);

            _printingDispatcher.AddTask(task);
        }

        UpdateListOfPrinters();
    }

    private void OnRemoveTaskButtonClick(object sender, EventArgs e)
    {
        if (printersListBox.SelectedIndex >= 0)
        {
            var printer = _editor.Printers[printersListBox.SelectedIndex];
            var task = _printingDispatcher.Tasks.FirstOrDefault(x => x.Printer == printer);

            if(task != null)
            {
                _printingDispatcher.RemoveTask(task);
            }
        }

        UpdateListOfPrinters();
    }
}