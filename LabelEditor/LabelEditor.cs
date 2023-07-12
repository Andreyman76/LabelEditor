namespace LabelEditor;
using LabelTemplate;
using System.Drawing.Drawing2D;
using System.Text;
using System.Xml.Linq;
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

    private LabelVariableBinder _binder = new();
    private XmlSerializer _serializer = new(typeof(PrinterLabel));

    public LabelEditor()
    {
        _binder.AddVariable("date", typeof(Utils), "DateTime", "dd.MM.yyyy");
        _binder.AddVariable("gs", typeof(Utils), "GS");
    }

    /// <summary>
    /// Получить XML текст для текущей этикетки
    /// </summary>
    /// <returns>XML текст текущей этикетки</returns>
    public string SaveLabelToXml()
    {
        var label = CurrentLabel.Clone() as PrinterLabel;
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

        CurrentLabel = _serializer.Deserialize(stream) as PrinterLabel;
    }

    public Bitmap GetCurrentLabelImage()
    {
        var label = _binder.BindAllVariables(CurrentLabel, new Utils());
       
        var dotsPerMm = 120.0f / 25.4f;
        var width = dotsPerMm * label.Size.Width;
        var height = dotsPerMm * label.Size.Height;
        var image = new Bitmap((int)width, (int)height);
        using var g = Graphics.FromImage(image);

        g.PageUnit = GraphicsUnit.Millimeter;
        g.SmoothingMode = SmoothingMode.None;
        g.CompositingQuality = CompositingQuality.HighSpeed;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;

        g.FillRectangle(Brushes.White, new RectangleF(0, 0, label.Size.Width, label.Size.Height));

        foreach (var element in label.Elements)
        {
            element.Draw(g);
        }

        return image;
    }

    public void PrintCurrentLabel(string printerName)
    {
        var label = _binder.BindAllVariables(CurrentLabel, new Utils());
        label.Print(printerName);
    }
}