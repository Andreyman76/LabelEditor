using LabelApi;
using PrintingApi;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace LabelEditorApi;

public class LabelEditor
{
    public ReadOnlyCollection<Type> RegisteredTypes { get => _registeredTypes.AsReadOnly(); }
    public ReadOnlyCollection<LabelVariableBinding> Variables { get => _variables.AsReadOnly(); }
    public ReadOnlyCollection<IPrinterDescription> Printers { get => _printers.AsReadOnly(); }

    public string VariablesJsonFilePath = "LabelVariables.json";
    public string PrintersJsonFilePath = "Printers.json";

    private List<Type> _registeredTypes = new()
    {
        typeof(BuiltInVariables)
    };

    private List<LabelVariableBinding> _variables = new List<LabelVariableBinding>();
    private List<IPrinterDescription> _printers = new List<IPrinterDescription>();
    private LabelVariableBinding _gs = new LabelVariableBinding()
    {
        IsBuiltIn = true,
        Description = "Разделитель ASCII 29",
        Name = "gs",
        TargetType = typeof(BuiltInVariables).FullName ?? throw new Exception($"Getting full name of target type {typeof(BuiltInVariables)} failed"),
        TargetAssembly = typeof(BuiltInVariables).Assembly.FullName ?? throw new Exception($"Getting full name of target type {typeof(BuiltInVariables)} failed"),
        PropertyName = nameof(BuiltInVariables.GS)
    };
    private BuiltInVariables _builtInVariables = new();

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
        _variables.Add(_gs);
    }

    public void RegisterType(Type type)
    {
        if (_registeredTypes.Contains(type) == false)
        {
            _registeredTypes.Add(type);
        }
    }

    public void UnregisterType(Type type)
    {
        _registeredTypes.Remove(type);
    }

    public void AddPrinter(IPrinterDescription printer)
    {
        _printers.Add(printer);
    }

    public void RemovePrinter(IPrinterDescription printer)
    {
        _printers.Remove(printer);
    }

    public void LoadVariablesFromJson()
    {
        if (File.Exists(VariablesJsonFilePath))
        {
            var json = File.ReadAllText(VariablesJsonFilePath, Encoding.UTF8);
            var variables = JsonSerializer.Deserialize<List<LabelVariableBinding>>(json) ?? throw new Exception("Load variables from JSON failed");

            _variables.Clear();
            _variables.Add(_gs);
            _variables.AddRange(variables);
        }
    }

    public void SaveVariablesToJson()
    {
        var json = JsonSerializer.Serialize(_variables.Where(x => x.IsBuiltIn == false), new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        File.WriteAllText(VariablesJsonFilePath, json, Encoding.UTF8);
    }

    public void SavePrintersToJson()
    {
        var printers = _printers.Select(x => x.GetPrinterDescription()).ToList();

        var json = JsonSerializer.Serialize(printers, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        File.WriteAllText(PrintersJsonFilePath, json);
    }

    public void LoadPrintersFromJson()
    {
        if (File.Exists(PrintersJsonFilePath))
        {
            var json = File.ReadAllText(PrintersJsonFilePath, Encoding.UTF8);
            var printers = JsonSerializer.Deserialize<List<PrinterDescription>>(json) ?? throw new Exception("Load printers from JSON failed");

            _printers = printers.Select( x => x.GetPrinterDescription()).ToList();
        }
    }

    /// <summary>
    /// Получить XML текст для текущей этикетки
    /// </summary>
    /// <returns>XML текст текущей этикетки</returns>
    public string SaveLabelToXml()
    {
        var label = LabelTemplate.Clone() as PrinterLabel ?? throw new Exception("Cloning label failed");
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

        LabelTemplate = _serializer.Deserialize(stream) as PrinterLabel ?? throw new Exception("Load label from XML failed");
    }

    public PrinterLabel GetCurrentLabel(IEnumerable<object> targetObjects)
    {
        var label = BindAllVariables(LabelTemplate, targetObjects);

        return label;
    }

    public void AddVariable(Type targetType, string propertyName, string? variableName = null, string? format = null)
    {
        var name = variableName ?? CreateNewVariableName();

        _variables.Add(new()
        {
            Name = name,
            TargetType = targetType.FullName ?? throw new Exception($"Getting full name of target type {targetType} failed"),
            TargetAssembly = targetType.Assembly.FullName ?? throw new Exception($"Getting assembly of target type {targetType} failed"),
            PropertyName = propertyName,
            Format = format
        });

        RenameVariableDublicates();
    }

    public void RemoveVariable(string variableName)
    {
        _variables.RemoveAll(x => x.Name == variableName && x.IsBuiltIn == false);
    }

    public void RenameVariableDublicates()
    {
        foreach (var variable in _variables)
        {
            if (_variables.Count(x => x.Name == variable.Name) != 1)
            {
                var newName = CreateNewVariableName();
                _variables.Last(x => x.Name == variable.Name && x.IsBuiltIn == false).Name = newName;

                throw new Exception($"Variable with name \"{variable.Name}\" is already exists (renamed to {newName})");
            }
        }
    }

    public string InsertVariable(string data, int position, string variableName)
    {
        return data.Insert(position, $"${{{variableName}}}");
    }

    private PrinterLabel BindAllVariables(PrinterLabel template, IEnumerable<object> targetObjects)
    {
        var label = template.Clone() as PrinterLabel ?? throw new Exception("Cloning label failed");
        var objects = new List<object>()
        {
            _builtInVariables
        };

        objects.AddRange(targetObjects);

        foreach(var target in objects)
        {
            var type = target.GetType();

            if (_registeredTypes.Contains(type) == false)
            {
                throw new Exception($"Type {type.FullName} is not registered in {nameof(LabelEditor)}");
            }

            var variables = _variables.Where(x => x.TargetType == target.GetType().FullName);

            foreach(var variable in variables)
            {
                label.Replace($"${{{variable.Name}}}", variable.GetStringFrom(target) ?? string.Empty);
            }
        }

        return label;
    }

    private string CreateNewVariableName()
    {
        var i = _variables.Count - 1;

        while (true)
        {
            var name = "var" + i;

            if (_variables.Count(x => x.Name == name) == 0)
            {
                return name;
            }

            i++;
        }
    }
}