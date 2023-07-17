using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace LabelApi;

/// <summary>
/// Переменная этикетки
/// </summary>
public class LabelVariable
{
    /// <summary>
    /// Отображаемое имя
    /// </summary>
    [Browsable(true)]
    [Description("Отображаемое имя")]
    [DisplayName("Имя переменной"), Category("Переменная")]
    public string Name { get; set; } = "var";

    /// <summary>
    /// Формат строки для значения свойства
    /// </summary>
    [Browsable(true)]
    [Description("Формат преобразования")]
    [DisplayName("Формат"), Category("Переменная")]
    public string? Format { get; set; }

    /// <summary>
    /// Свойство целевого объекта
    /// </summary>
    [ReadOnly(true)]
    [Browsable(true)]
    [Description("Свойство целевого объекта")]
    [DisplayName("Свойство"), Category("Переменная")]
    public string PropertyName { get; init; } = string.Empty;

    /// <summary>
    /// Тип целевого объекта
    /// </summary>
    [ReadOnly(true)]
    [Browsable(true)]
    [Description("Тип целевого объекта")]
    [DisplayName("Тип оъекта"), Category("Переменная")]
    public string TargetType { get; set; } = string.Empty;

    /// <summary>
    /// Сборка целевого объекта
    /// </summary>
    [Browsable(false)]
    public string TargetAssembly { get; set; } = string.Empty;

    /// <summary>
    /// Описание переменной
    /// </summary>
    [Browsable(true)]
    [Description("Описание переменной")]
    [DisplayName("Описание"), Category("Переменная")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Встроенная ли переменная (встроенные переменные нельзя удалять или изменять)
    /// </summary>
    [Browsable(false)]
    [JsonIgnore]
    public bool IsBuiltIn { get; init; } = false;

    /// <summary>
    /// Получить строковое представление переменной с учетов формата на основе целевого свойства
    /// </summary>
    /// <param name="value">Объект целевого ствойства</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string? GetStringFrom(object value)
    {
        var type = Type.GetType(TargetType + ", " + TargetAssembly) ?? throw new Exception($"Type {TargetType} not found");
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

        return toStringMethod == null
            ? throw new Exception($"{propertyType.FullName} does not have a formatted ToString method")
            : (toStringMethod.Invoke(propertyValue, new object[] { Format })?.ToString());
    }

    /// <summary>
    /// Форматирование строки
    /// </summary>
    /// <param name="src">Исзодная строка</param>
    /// <param name="format">Формат (0 - символ исходной строки, любой другой символ - без изменений)</param>
    /// <returns></returns>
    private static string FormatString(string? src, string format)
    {
        if (string.IsNullOrEmpty(src))
        {
            return format;
        }

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