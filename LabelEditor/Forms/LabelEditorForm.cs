using LabelTemplate;

namespace LabelEditor;

public partial class LabelEditorForm : Form
{
    private object[] _targetObjects = new object[] { new LabelUtils() };

    private LabelEditor _editor = new()
    {
        RegisteredTypes = new()
        {
            typeof(LabelUtils),
            typeof(Gtin),
            typeof(Pallet)
        }
    };

    public LabelEditorForm()
    {
        InitializeComponent();
        _editor.LoadVariablesFromJson();
        _editor.LoadPrintersFromJson();
        labelPropertyGrid.SelectedObject = _editor.LabelTemplate;
        UpdateListOfPrinters();
        Redraw();
    }

    private void OnPrintersToolStripMenuItemClick(object sender, EventArgs e)
    {
        var form = new PrintersEditorForm(_editor);
        form.ShowDialog();

        UpdateListOfPrinters();
    }

    private void OnSaveToolStripMenuItemClick(object sender, EventArgs e)
    {
        var dialog = new SaveFileDialog()
        {
            DefaultExt = "xml",
            Filter = "XML Files|*.xml;"
        };

        var result = dialog.ShowDialog(this);

        if (result == DialogResult.OK)
        {
            var xml = _editor.SaveLabelToXml();
            File.WriteAllText(dialog.FileName, xml);
        }
    }

    private void OnLoadToolStripMenuItemClick(object sender, EventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            DefaultExt = "xml",
            Filter = "XML Files|*.xml;"
        };

        var result = dialog.ShowDialog(this);

        if (result == DialogResult.OK)
        {
            var xml = File.ReadAllText(dialog.FileName);
            _editor.LoadLabelFromXml(xml);
        }

        UpdateListOfObjects();
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

    private void OnAddTextButtonClick(object sender, EventArgs e)
    {
        _editor.LabelTemplate.Elements.Add(
            new LabelText()
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
           new LabelDataMatrix()
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
           new LabelCode128()
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
            new LabelImage()
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
           new LabelEllipse()
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

        foreach (var name in _editor.Printers.Select(x => x.Name))
        {
            printersListBox.Items.Add(name);
        }
    }

    private void Redraw()
    {
        renderedLabelPictureBox.BackColor = Color.Gray;
        renderedLabelPictureBox.Image = _editor.GetCurrentLabel(_targetObjects).GetImage(new Point(600, 600));
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

    private void OnEditVariablesToolStripMenuItemClick(object sender, EventArgs e)
    {
        var varEditor = new VariableEditorForm(_editor);

        varEditor.ShowDialog();
        Redraw();
    }

    private void OnPrintButtonClick(object sender, EventArgs e)
    {
        if (printersListBox.SelectedIndex >= 0)
        {
            using var printer = _editor.Printers[printersListBox.SelectedIndex].CreatePrinter();

            for (int i = 0; i < 5; i++)
            {
                printer.Print(_editor.GetCurrentLabel(_targetObjects));
            }
        }
    }

    private void OnDataSourceToolStripMenuItemClick(object sender, EventArgs e)
    {
        var form = new DataSourceSelectionForm();
        form.ShowDialog();
    }
}