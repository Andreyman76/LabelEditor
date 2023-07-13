using System.Reflection;

namespace LabelEditor;

public partial class VariableEditorForm : Form
{
    private List<Type> _registeredTypes;
    private LabelVariableBinder _binder;

    public VariableEditorForm(List<Type> registeredTypes, LabelVariableBinder binder)
    {
        InitializeComponent();

        _registeredTypes = registeredTypes;
        _binder = binder;

        foreach (var type in _registeredTypes)
        {
            classesListBox.Items.Add(type.FullName);
        }

        UpdateVariablesListBox();
    }

    private void OnCreateVariableButtonClick(object sender, EventArgs e)
    {
        if (classesListBox.SelectedIndex >= 0 && propertiesListBox.SelectedIndex >= 0)
        {
            var type = _registeredTypes[classesListBox.SelectedIndex];
            var properties = type.GetProperties();
            var property = properties[propertiesListBox.SelectedIndex];

            _binder.AddVariable("var", type, property.Name);

            UpdateVariablesListBox();
        }
    }

    public void UpdateVariablesListBox()
    {
        variablesListBox.Items.Clear();

        foreach (var variable in _binder.Variables)
        {
            variablesListBox.Items.Add(variable.Name);
        }
    }

    private void OnClassesListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (classesListBox.SelectedIndex >= 0)
        {
            var type = _registeredTypes[classesListBox.SelectedIndex];

            var properties = type.GetProperties();
            propertiesListBox.Items.Clear();

            foreach (var property in properties)
            {
                propertiesListBox.Items.Add(property.Name);
            }
        }
    }

    private void OnVariablesListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (variablesListBox.SelectedIndex >= 0)
        {
            variablePropertyGrid.SelectedObject = _binder.Variables[variablesListBox.SelectedIndex];
        }
    }

    private void OnVariablePropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        UpdateVariablesListBox();
    }
}
