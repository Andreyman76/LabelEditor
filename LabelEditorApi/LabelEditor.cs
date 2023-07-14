using LabelApi;
using PrintingApi;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace LabelEditorApi;

public class LabelEditor
{
    public List<LabelVariableBinding> Variables { get; set; } = new();
    public List<Type> RegisteredTypes { get; set; } = new();
    public List<IPrinterDescription> Printers { get; set; } = new();

    /// <summary>
    /// Текущая этикетка редактора
    /// </summary>
    public PrinterLabel LabelTemplate { get; set; } = new()
    {
        Size = new(22, 22)
    };

    private XmlSerializer _serializer = new(typeof(PrinterLabel));

    public LabelEditor()
    {
        AddVariable("date", typeof(BuiltInVariables), nameof(BuiltInVariables.CurrentDateTime), "dd.MM.yyyy");
        AddVariable("gs", typeof(BuiltInVariables), nameof(BuiltInVariables.GS));
    }

    public void LoadVariablesFromJson()
    {
        if (File.Exists("LabelVariables.json"))
        {
            var json = File.ReadAllText("LabelVariables.json", Encoding.UTF8);
            var variables = JsonSerializer.Deserialize<List<LabelVariableBinding>>(json);

            Variables = variables;
        }
    }

    public void SaveVariablesToJson()
    {
        var json = JsonSerializer.Serialize(Variables, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        File.WriteAllText("LabelVariables.json", json, Encoding.UTF8);
    }

    public void SavePrintersToJson()
    {
        var printers = Printers.Select(x => x.GetPrinterDescription()).ToList();

        var json = JsonSerializer.Serialize(printers, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        File.WriteAllText("Printers.json", json);
    }

    public void LoadPrintersFromJson()
    {
        if (File.Exists("Printers.json"))
        {
            var json = File.ReadAllText("Printers.json", Encoding.UTF8);
            var printers = JsonSerializer.Deserialize<List<PrinterDescription>>(json);

            Printers = printers.Select( x => x.GetPrinterDescription()).ToList();
        }
    }

    /// <summary>
    /// Получить XML текст для текущей этикетки
    /// </summary>
    /// <returns>XML текст текущей этикетки</returns>
    public string SaveLabelToXml()
    {
        var label = LabelTemplate.Clone() as PrinterLabel;
        label.Replace("\u001d", "${gs}");

        using var stream = new MemoryStream();
        _serializer.Serialize(stream, label);

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// Загрузить текущую этикетку из XML текста
    /// </summary>
    /// <param name="xmlText">XML текст сериализованной этикетки</param>
    public void LoadLabelFromXml(string xmlText)
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlText));

        LabelTemplate = _serializer.Deserialize(stream) as PrinterLabel;
    }

    public PrinterLabel GetCurrentLabel(IEnumerable<object> targetObjects)
    {
        var label = BindAllVariables(LabelTemplate, targetObjects);

        return label;
    }

    public void AddVariable(string variableName, Type targetType, string propertyName, string? format = null)
    {
        Variables.Add(new()
        {
            Name = variableName,
            TargetType = targetType.FullName,
            PropertyName = propertyName,
            Format = format
        });

        RenameDublicates();
    }

    public void RemoveVariable(string variableName)
    {
        Variables.RemoveAll(x => x.Name == variableName);
    }

    public void RenameDublicates()
    {
        foreach (var variable in Variables)
        {
            if (Variables.Count(x => x.Name == variable.Name) != 1)
            {
                var newName = GetNewVariableName();
                Variables.Last(x => x.Name == variable.Name).Name = newName;

                throw new Exception($"Variable with name \"{variable.Name}\" is already exists (renamed to {newName})");
            }
        }
    }

    public string InsertVariable(string data, int position, string variableName)
    {
        return data.Insert(position, $"${{{variableName}}}");
    }

    public PrinterLabel BindAllVariables(PrinterLabel template, IEnumerable<object> targetObjects)
    {
        var label = template.Clone() as PrinterLabel;

        foreach(var target in targetObjects)
        {
            var variables = Variables.Where(x => x.TargetType == target.GetType().FullName);

            foreach(var variable in variables)
            {
                label.Replace($"${{{variable.Name}}}", variable.GetStringFrom(target) ?? string.Empty);
            }
        }

        return label;
    }

    public string GetNewVariableName()
    {
        var i = Variables.Count - 1;

        while (true)
        {
            var name = "var" + i;

            if (Variables.Count(x => x.Name == name) == 0)
            {
                return name;
            }

            i++;
        }
    }
}