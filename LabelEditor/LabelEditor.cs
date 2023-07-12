namespace LabelEditor;
using LabelTemplate;
using System.Text;
using System.Xml.Serialization;

public class LabelEditor
{
    /// <summary>
    /// Текущая этикетка редактора
    /// </summary>
    public PrinterLabel CurrentLabel { get; set; } = new()
    {
        Size = new(22, 22)
    };

    private XmlSerializer _serializer = new(typeof(PrinterLabel));

    /// <summary>
    /// Получить XML текст для текущей этикетки
    /// </summary>
    /// <returns>XML текст текущей этикетки</returns>
    public string SaveLabelToXml()
    {
        var label = CurrentLabel.Clone() as PrinterLabel;
        label.Replace("\u001d", "$GS");

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

        CurrentLabel = _serializer.Deserialize(stream) as PrinterLabel;
        CurrentLabel.Replace("$GS", "\u001d");
    }

    ///// <summary>
    ///// <para>Возвращает элемент этикетки по заданным координатам</para>
    ///// <para>Элемент считается найденным, если указанные координаты находятся внутри границы элемента</para>
    ///// </summary>
    ///// <param name="g"></param>
    ///// <param name="position">Координаты для поиска</param>
    ///// <returns></returns>
    //public LabelElementBase? FindElementByPosition(Graphics g, Point position)
    //{
    //    if (CurrentLabel != null)
    //    {
    //        foreach (var element in CurrentLabel.Elements)
    //        {
    //            if (element.GetComputedBounds(g).Contains(position))
    //            {
    //                return element;
    //            }
    //        }
    //    }

    //    return null;
    //}
}