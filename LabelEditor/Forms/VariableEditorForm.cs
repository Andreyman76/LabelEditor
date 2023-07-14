using System.Text.Json;

namespace LabelEditor;

public partial class VariableEditorForm : Form
{
    private LabelEditor _editor;

    public VariableEditorForm(LabelEditor editor)
    {
        InitializeComponent();
        objectPropertiesGridView.Columns.Add("Name", "Имя");
        objectPropertiesGridView.Columns.Add("Type", "Тип");

        _editor = editor;

        foreach (var type in _editor.RegisteredTypes)
        {
            classesListBox.Items.Add(type.FullName);
        }

        UpdateVariablesListBox();
    }

    private void OnCreateVariableButtonClick(object sender, EventArgs e)
    {
        if (classesListBox.SelectedIndex >= 0 && objectPropertiesGridView.SelectedCells.Count != 0)
        {
            var row = objectPropertiesGridView.SelectedCells[0].OwningRow;
            var name = row.Cells["Name"].EditedFormattedValue.ToString();
            var type = _editor.RegisteredTypes[classesListBox.SelectedIndex];
            _editor.AddVariable(_editor.GetNewVariableName(), type, name);
            UpdateVariablesListBox();
            variablesListBox.SelectedIndex = variablesListBox.Items.Count - 1;
        }
    }

    private void OnDeleteVariableButtonClick(object sender, EventArgs e)
    {
        var item = variablePropertyGrid.SelectedGridItem;
        var name = item.PropertyDescriptor?.Name;

        if (variablesListBox.SelectedIndex >= 0)
        {
            _editor.RemoveVariable(variablesListBox.Items[variablesListBox.SelectedIndex].ToString());
            variablePropertyGrid.SelectedObject = null;
            UpdateVariablesListBox();
        }
    }

    public void UpdateVariablesListBox()
    {
        variablesListBox.Items.Clear();

        foreach (var variable in _editor.Variables)
        {
            variablesListBox.Items.Add(variable.Name);
        }
    }

    private void OnClassesListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (classesListBox.SelectedIndex >= 0)
        {
            var type = _editor.RegisteredTypes[classesListBox.SelectedIndex];

            var properties = type.GetProperties();
            objectPropertiesGridView.Rows.Clear();

            foreach (var property in properties)
            {
                objectPropertiesGridView.Rows.Add(property.Name, property.PropertyType);
            }
        }
    }

    private void OnVariablesListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (variablesListBox.SelectedIndex >= 0)
        {
            variablePropertyGrid.SelectedObject = _editor.Variables[variablesListBox.SelectedIndex];
        }
    }

    private void OnVariablePropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        try
        {
            _editor.RenameDublicates();
        }
        finally
        {
            UpdateVariablesListBox();
        }
    }

    private void OnVariableEditorFormFormClosing(object sender, FormClosingEventArgs e)
    {
        _editor.SaveVariablesToJson();
    }
}