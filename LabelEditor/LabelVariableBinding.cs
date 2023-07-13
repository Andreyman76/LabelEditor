using System.ComponentModel;
using System.Text;

namespace LabelEditor;

public class LabelVariableBinding
{
    [Browsable(true)]
    [Description("Отображаемое имя")]
    [DisplayName("Имя переменной"), Category("Переменная")]
    public string Name { get; set; } = "var";

    [Browsable(true)]
    [Description("Формат преобразования")]
    [DisplayName("Формат"), Category("Переменная")]
    public string? Format { get; set; }

    [ReadOnly(true)]
    [Browsable(true)]
    [Description("Свойство целевого объекта")]
    [DisplayName("Свойство"), Category("Переменная")]
    public string PropertyName { get; init; } = string.Empty;

    [ReadOnly(true)]
    [Browsable(true)]
    [Description("Тип целевого объекта")]
    [DisplayName("Тип оъекта"), Category("Переменная")]
    public string TargetType { get; set; } = "System.String";

    [Browsable(true)]
    [Description("Описание переменной")]
    [DisplayName("Описание"), Category("Переменная")]
    public string Description { get; set; } = string.Empty;

    public string? GetStringFrom(object value)
    {
        var type = Type.GetType(TargetType);
        var property = type.GetProperty(PropertyName)
            ?? throw new Exception($"Property {PropertyName} not found in {TargetType}");

        var propertyValue = property.GetValue(value);

        if (propertyValue == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(Format))
        {
            return propertyValue.ToString();
        }

        var propertyType = propertyValue.GetType();

        if (propertyType == typeof(string))
        {
            return FormatString(propertyValue as string, Format);
        }

        var toStringMethod = propertyType.GetMethod("ToString", new[] { typeof(string) });

        if (toStringMethod == null)
        {
            throw new Exception($"{propertyType.FullName} does not have a formatted ToString method");
        }

        return toStringMethod.Invoke(propertyValue, new object[] { Format })?.ToString();
    }

    private static string FormatString(string src, string format)
    {
        var sb = new StringBuilder();
        var i = src.Length - 1;

        foreach(var c in format.Reverse())
        {
            if(c == '0' && i > 0)
            {
                sb.Insert(0, src[i--]);
            }
            else
            {
                sb.Insert(0, c);
            }
        }

        return sb.ToString();
    }
}