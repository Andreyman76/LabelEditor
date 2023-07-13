using LabelTemplate;

namespace LabelEditor;

public class LabelVariableBinder
{
    public List<LabelVariableBinding> Variables { get; set; } = new();

    public void AddVariable(string variableName, Type targetType, string propertyName, string? format = null)
    {
        if(Variables.Count(x => x.Name == variableName) != 0)
        {
            throw new Exception($"Variable with name \"{variableName}\" is already exists");
        }

        Variables.Add(new()
        {
            Name = variableName,
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
        Variables.RemoveAll(x=> x.Name == variableName);
    }

    public PrinterLabel BindAllVariables(PrinterLabel template, object target)
    {
        var label =  template.Clone() as PrinterLabel;
        
        foreach(var variable in Variables)
        {
            if(variable.TargetType == target.GetType())
            {
                label.Replace($"${{{variable.Name}}}", variable.GetStringFrom(target) ?? string.Empty);
            }      
        }

        return label;
    }
}