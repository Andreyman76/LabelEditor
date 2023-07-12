namespace LabelEditor;

public class LabelVariableBinding
{
    public string? Format { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public Type TargetType { get; set; } = typeof(string);

    public string? GetStringFrom(object value)
    {
        var property = TargetType.GetProperty(PropertyName) 
            ?? throw new Exception($"Property {PropertyName} not found in {TargetType.FullName}");

        var propertyValue = property.GetValue(value);

        if(propertyValue == null)
        {
            return null;
        }
   
        if(string.IsNullOrEmpty(Format))
        {
            return propertyValue.ToString();
        }
        else
        {
            var propertyType = propertyValue.GetType();
            var toStringMethod = propertyType.GetMethod("ToString", new[] { typeof(string) }) 
                ?? throw new Exception($"{propertyType.FullName} does not have a formatted ToString method");

            return toStringMethod.Invoke(propertyValue, new object[] { Format })?.ToString();
        }
    }
}