using LabelTemplate;
using System.Windows.Forms;

namespace LabelEditor;

public class LabelVariableBinder
{
    private Dictionary<string, LabelVariableBinding> _bindings = new();

    public void AddVariable(string variableName, Type targetType, string propertyName, string? format = null)
    {
        _bindings.Add(variableName, new()
        {
            TargetType = targetType,
            PropertyName = propertyName,
            Format = format
        });
    }

    public string AddVariable(string data, int position, string variableName)
    {
        return data.Insert(position, $"${{{variableName}}}");
    }

    public void RemoveVariable(string variableName)
    {
        _bindings.Remove(variableName);
    }

    public PrinterLabel BindAllVariables(PrinterLabel template, object target)
    {
        var label =  template.Clone() as PrinterLabel;
        
        foreach(var binding in _bindings)
        {
            label.Replace($"${{{binding.Key}}}", binding.Value.GetStringFrom(target) ?? string.Empty);
        }

        return label;
    }
}