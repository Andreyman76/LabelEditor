using LabelApi;
using PrintingApi;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Serialization;

namespace LabelEditorApi;

/// <summary>
/// Редактор этикеток
/// </summary>
public class LabelEditor
{
    /// <summary>
    /// Текущая этикетка редактора
    /// </summary>
    public PrinterLabel LabelTemplate { get; set; } = new()
    {
        Size = new(22, 22)
    };

    /// <summary>
    /// Зарегистрированные типы данных
    /// </summary>
    public ReadOnlyCollection<Type> RegisteredTypes { get => _registeredTypes.AsReadOnly(); }

    /// <summary>
    /// Переменные
    /// </summary>
    public ReadOnlyCollection<LabelVariable> Variables { get => _variables.AsReadOnly(); }

    /// <summary>
    /// Дескрипторы принтеров
    /// </summary>
    public ReadOnlyCollection<IPrinterDescription> Printers { get => _printers.AsReadOnly(); }

    private readonly BuiltInVariables _builtInVariables = new();
    private readonly XmlSerializer _serializer = new(typeof(PrinterLabel));
    private readonly List<LabelVariable> _variables = new();
    private List<IPrinterDescription> _printers = new();

    private readonly LabelVariable _gs = new()
    {
        IsBuiltIn = true,
        Description = "Разделитель ASCII 29",
        Name = "gs",
        TargetType = typeof(BuiltInVariables).FullName ?? throw new Exception($"Getting full name of target type {typeof(BuiltInVariables)} failed"),
        TargetAssembly = typeof(BuiltInVariables).Assembly.FullName ?? throw new Exception($"Getting full name of target type {typeof(BuiltInVariables)} failed"),
        PropertyName = nameof(BuiltInVariables.GS)
    };

    private readonly List<Type> _registeredTypes = new()
    {
        typeof(BuiltInVariables)
    };

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        WriteIndented = true
    };

    public LabelEditor()
    {
        _variables.Add(_gs);
    }

    /// <summary>
    /// Зарегистрировать тип
    /// </summary>
    /// <param name="type">Тип данных</param>
    public void RegisterType(Type type)
    {
        if (_registeredTypes.Contains(type) == false)
        {
            _registeredTypes.Add(type);
        }
    }

    /// <summary>
    /// Удалить тип
    /// </summary>
    /// <param name="type">Тип данных</param>
    public void UnregisterType(Type type)
    {
        _registeredTypes.Remove(type);
    }

    /// <summary>
    /// Добавить принтер
    /// </summary>
    /// <param name="printer">Дескриптор принтера</param>
    public void AddPrinter(IPrinterDescription printer)
    {
        _printers.Add(printer);
    }

    /// <summary>
    /// Удалить принтер
    /// </summary>
    /// <param name="printer">Дескриптор принтера</param>
    public void RemovePrinter(IPrinterDescription printer)
    {
        _printers.Remove(printer);
    }

    /// <summary>
    /// Сохранить переменные в JSON файл
    /// </summary>
    /// <param name="filePath">Путь к JSON файлу</param>
    public void SaveVariablesToJson(string filePath)
    {
        var json = JsonSerializer.Serialize(_variables.Where(x => x.IsBuiltIn == false), _serializerOptions);

        File.WriteAllText(filePath, json, Encoding.UTF8);
    }

    /// <summary>
    /// Загрузить переменные из JSON файла
    /// </summary>
    /// <param name="filePath">Путь к JSON файлу</param>
    /// <exception cref="Exception"></exception>
    public void LoadVariablesFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath, Encoding.UTF8);
            var variables = JsonSerializer.Deserialize<List<LabelVariable>>(json) ?? throw new Exception("Load variables from JSON failed");

            _variables.Clear();
            _variables.Add(_gs);
            _variables.AddRange(variables);
        }
    }

    /// <summary>
    /// Сохранить принтеры в JSON файл
    /// </summary>
    /// <param name="filePath">Путь к JSON файлу</param>
    public void SavePrintersToJson(string filePath)
    {
        var printers = _printers.Select(x => x.GetPrinterDescription()).ToList();

        var json = JsonSerializer.Serialize(printers, _serializerOptions);

        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Загрузить принтеры из JSON файла
    /// </summary>
    /// <param name="filePath">Путь к JSON файлу</param>
    /// <exception cref="Exception"></exception>
    public void LoadPrintersFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath, Encoding.UTF8);
            var printers = JsonSerializer.Deserialize<List<SerialazablePrinterDescription>>(json) ?? throw new Exception("Load printers from JSON failed");

            _printers = printers.Select(x => x.GetPrinterDescription()).ToList();
        }
    }

    /// <summary>
    /// Сохранить текущую этикетку в XML файл
    /// </summary>
    /// <param name="filePath">Путь к XML файлу</param>
    /// <exception cref="Exception"></exception>
    public void SaveLabelToXml(string filePath)
    {
        var label = LabelTemplate.Clone() as PrinterLabel ?? throw new Exception("Cloning label failed");
        label.Replace("\u001d", "${gs}");

        using var stream = new MemoryStream();
        _serializer.Serialize(stream, label);

        var xml = Encoding.UTF8.GetString(stream.ToArray());
        File.WriteAllText(filePath, xml);
    }

    /// <summary>
    /// Загрузить текущую этикетку из XML файла
    /// </summary>
    /// <param name="filePath">Путь к XML файлу</param>
    /// <exception cref="Exception"></exception>
    public void LoadLabelFromXml(string filePath)
    {
        var xml = File.ReadAllText(filePath);
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

        LabelTemplate = _serializer.Deserialize(stream) as PrinterLabel ?? throw new Exception("Load label from XML failed");
    }

    /// <summary>
    /// Получить текущую этикетку с установленными значениями переменных
    /// </summary>
    /// <param name="targetObjects"></param>
    /// <returns></returns>
    public PrinterLabel GetCurrentLabel(IEnumerable<object> targetObjects)
    {
        var label = BindAllVariables(LabelTemplate, targetObjects);

        return label;
    }

    /// <summary>
    /// Добавить переменную
    /// </summary>
    /// <param name="targetType">Тип целевого объекта</param>
    /// <param name="propertyName">Свойство целевого объекта</param>
    /// <param name="variableName">Имя переменной</param>
    /// <param name="format">Формат строки для значения свойства</param>
    /// <exception cref="Exception"></exception>
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

        RenameVariableDublicate();
    }

    /// <summary>
    /// Удалить переменную
    /// </summary>
    /// <param name="variableName">Имя переменной</param>
    public void RemoveVariable(string? variableName)
    {
        _variables.RemoveAll(x => x.Name == variableName && x.IsBuiltIn == false);
    }

    /// <summary>
    /// Переименовать дубликат переменной
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void RenameVariableDublicate()
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

    /// <summary>
    /// Установить значения всем переменным для этикетки
    /// </summary>
    /// <param name="template">Шаблон этикетки</param>
    /// <param name="targetObjects">Объекты, из которых берутся значения переменных (тип объекта должен быть зарегистрирован)</param>
    /// <returns>Этикетка с установленными значениями всех переменных</returns>
    /// <exception cref="Exception"></exception>
    private PrinterLabel BindAllVariables(PrinterLabel template, IEnumerable<object> targetObjects)
    {
        foreach (var target in targetObjects)
        {
            var type = target.GetType();

            if (_registeredTypes.Contains(type) == false)
            {
                throw new Exception($"Type {type.FullName} is not registered in {nameof(LabelEditor)}");
            }
        }

        var label = template.Clone() as PrinterLabel ?? throw new Exception("Cloning label failed");
        label.BindVariables(Variables, targetObjects);
      
        return label;
    }

    /// <summary>
    /// Создать новое уникальное имя для переменной
    /// </summary>
    /// <returns>Уникальное имя переменной</returns>
    private string CreateNewVariableName()
    {
        var i = _variables.Count - 1;

        while (true)
        {
            var name = "var" + i;

            if (_variables.Any(x => x.Name == name) == false)
            {
                return name;
            }

            i++;
        }
    }
}