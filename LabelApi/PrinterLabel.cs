using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Xml.Serialization;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Этикетка
/// </summary>
[XmlRoot("Label")]
public class PrinterLabel : ICloneable
{
    /// <summary>
    /// Размер этикетки в мм
    /// </summary>
    [Browsable(true)]
    [Description("Размер этикетки в мм")]
    [DisplayName("Размер"), Category("Этикетка")]
    public PrintingSize Size { get; set; }

    /// <summary>
    /// Элементы этикетки
    /// </summary>
    [XmlArrayItem(typeof(LabelText))]
    [XmlArrayItem(typeof(LabelImage))]
    [XmlArrayItem(typeof(LabelDataMatrix))]
    [XmlArrayItem(typeof(LabelCode128))]
    [XmlArrayItem(typeof(LabelEllipse))]
    [Browsable(false)]
    public List<LabelElementBase> Elements { get; set; } = new();

    private bool _printed = false;
    private static readonly BuiltInVariables _builtInVariables = new();

    /// <summary>
    /// Получить срендеренное изображение этикетки
    /// </summary>
    /// <param name="dpi">Разрешение печати</param>
    /// <returns></returns>
    public Bitmap GetImage(Dpi dpi)
    {
        var dotsPerMmX = dpi.X / 25.4f;
        var dotsPerMmY = dpi.Y / 25.4f;
        var width = dotsPerMmX * Size.Width;
        var height = dotsPerMmY * Size.Height;
        var image = new Bitmap((int)width, (int)height);
        image.SetResolution(dpi.X, dpi.Y);

        using var g = Graphics.FromImage(image);
        g.Clear(Color.White);
        g.PageUnit = GraphicsUnit.Millimeter;
        g.SmoothingMode = SmoothingMode.None;
        g.CompositingQuality = CompositingQuality.HighSpeed;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;

        foreach (var element in Elements)
        {
            if (element is LabelText)
            {
                SetLabelTextAutosize(element as LabelText);
            }

            element.Draw(g);
        }

        return image;
    }

    /// <summary>
    /// Заменить значение в текстовых данных всех элементов этикетки
    /// </summary>
    /// <param name="from">Что заменить</param>
    /// <param name="to">На что заменить</param>
    public void Replace(string from, string to)
    {
        foreach (var element in Elements)
        {
            element.Replace(from, to);
        }
    }

    /// <summary>
    /// Полное копирование этикетки
    /// </summary>
    /// <returns>Полная копия этикетки</returns>
    /// <exception cref="Exception"></exception>
    public object Clone()
    {
        var result = new PrinterLabel()
        {
            Size = Size
        };

        foreach (var element in Elements)
        {
            result.Elements.Add(element.Clone() as LabelElementBase ?? throw new Exception($"Cloning LabelElement {element.Name} failed"));
        }

        return result;
    }

    /// <summary>
    /// Получить ZPL код этикетки
    /// </summary>
    /// <param name="dpi">Разрешение печати</param>
    /// <returns></returns>
    public string GetZpl(Dpi dpi)
    {
        using var image = GetImage(dpi);
        using var stream = new MemoryStream();

        image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        var zpl = PDFtoZPL.Conversion.ConvertBitmap(stream.ToArray());

        return zpl;
    }

    /// <summary>
    /// Печать в PDF
    /// </summary>
    /// <param name="printerName">Имя принтера</param>
    /// <returns></returns>
    public bool PrintToPdf(string printerName)
    {
        _printed = false;
        using var document = new PrintDocument();
        document.PrintPage += PrintPageHandler;
        document.PrinterSettings.PrinterName = printerName;
        document.Print();

        return _printed;
    }

    public void BindVariables(IEnumerable<LabelVariable> variables, IEnumerable<object> targetObjects)
    {
        var objects = new List<object>()
        {
            _builtInVariables
        };

        objects.AddRange(targetObjects);

        foreach (var target in objects)
        {
            var type = target.GetType();
            var vars = variables.Where(x => x.TargetType == target.GetType().FullName);

            foreach (var variable in vars)
            {
                Replace($"${{{variable.Name}}}", variable.GetStringFrom(target) ?? string.Empty);
            }
        }
    }

    /// <summary>
    /// Обработчик PrintDocument.PrintPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="Exception"></exception>
    private void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
        using var g = e.Graphics ?? throw new Exception("PrintPageEventArgs contains Graphics with null value");
        g.Clear(Color.White);
        g.PageUnit = GraphicsUnit.Millimeter;

        foreach (var element in Elements)
        {
            if (element is LabelText)
            {
                SetLabelTextAutosize(element as LabelText);
            }

            element.Draw(g);
        }

        _printed = true;
    }

    /// <summary>
    /// Установка размера для текстового элемента этикетки (используется для переноса слов в текста в случае превышения размера)
    /// </summary>
    /// <param name="labelText"></param>
    /// <exception cref="Exception"></exception>
    private void SetLabelTextAutosize(LabelText? labelText)
    {
        if(labelText == null)
        {
            throw new Exception("Set LabelText autosize failed");
        }

        var width = Size.Width - labelText.Position.X;
        var height = Size.Height - labelText.Position.Y;
        labelText.Size = new(width, height);
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы