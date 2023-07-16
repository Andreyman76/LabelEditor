using LabelEditorApi;
namespace AggregationCodesPrinter;

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
            classesListBox.Items.Add(type.FullName ?? throw new Exception($"Getting full name of target type {type} failed"));
        }

        UpdateVariablesListBox();
    }

    private void OnCreateVariableButtonClick(object sender, EventArgs e)
    {
        if (classesListBox.SelectedIndex >= 0 && objectPropertiesGridView.SelectedCells.Count != 0)
        {
            var row = objectPropertiesGridView.SelectedCells[0].OwningRow;
            var propertyName = row.Cells["Name"].EditedFormattedValue.ToString() ?? throw new Exception($"Getting property name from row failed");
            var type = _editor.RegisteredTypes[classesListBox.SelectedIndex];
            _editor.AddVariable(type, propertyName);
            UpdateVariablesListBox();
            variablesListBox.SelectedIndex = variablesListBox.Items.Count - 1;
        }
    }

    private void OnDeleteVariableButtonClick(object sender, EventArgs e)
    {
        var item = variablePropertyGrid.SelectedGridItem;
        var name = item?.PropertyDescriptor?.Name;

        if (variablesListBox.SelectedIndex >= 0)
        {
            var variableName = variablesListBox.Items[variablesListBox.SelectedIndex].ToString();
            _editor.RemoveVariable(variableName);
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
            var variable = _editor.Variables[variablesListBox.SelectedIndex];

            variablePropertyGrid.SelectedObject = variable;
            variablePropertyGrid.Enabled = variable.IsBuiltIn == false;
        }
    }

    private void OnVariablePropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        try
        {
            _editor.RenameVariableDublicate();
        }
        finally
        {
            UpdateVariablesListBox();
        }
    }

    private void OnVariableEditorFormFormClosing(object sender, FormClosingEventArgs e)
    {
        _editor.SaveVariablesToJson(ApplicationSettingsProvider.Settings.VariablesJsonFilePath);
    }
}