namespace LabelEditor;
using LabelTemplate;
using System.Text;
using System.Xml.Serialization;

public class LabelEditor
{
    /// <summary>
    /// Текущая этикетка редактора
    /// </summary>
    public PrinterLabel LabelTemplate { get; set; } = new()
    {
        Size = new(22, 22)
    };

    public LabelVariableBinder Binder { get; set; } = new();
    private XmlSerializer _serializer = new(typeof(PrinterLabel));

    public LabelEditor()
    {
        Binder.AddVariable("date", typeof(LabelUtils), "DateTime", "dd.MM.yyyy");
        Binder.AddVariable("gs", typeof(LabelUtils), "GS");
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

    public PrinterLabel GetCurrentLabel()
    {
        var label = Binder.BindAllVariables(LabelTemplate, new LabelUtils());

        return label;
    }
}