using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Xml.Serialization;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

[XmlRoot("Label")]
public class PrinterLabel : ICloneable
{
    [Browsable(true)]
    [Description("Размер этикетки в мм")]
    [DisplayName("Размер"), Category("Этикетка")]
    public PrintingSize Size { get; set; }

    [XmlArrayItem(typeof(LabelText))]
    [XmlArrayItem(typeof(LabelImage))]
    [XmlArrayItem(typeof(LabelDataMatrix))]
    [XmlArrayItem(typeof(LabelCode128))]
    [XmlArrayItem(typeof(LabelEllipse))]
    [Browsable(false)]
    public List<LabelElementBase> Elements { get; set; } = new();

    private bool _printed = false;

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

    public void Replace(string from, string to)
    {
        foreach (var element in Elements)
        {
            element.Replace(from, to);
        }
    }

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

    public string GetZpl(Dpi dpi)
    {
        using var image = GetImage(dpi);
        using var stream = new MemoryStream();

        image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        var zpl = PDFtoZPL.Conversion.ConvertBitmap(stream.ToArray());

        return zpl;
    }

    public bool PrintToPdf(string printerName)
    {
        _printed = false;
        using var document = new PrintDocument();
        document.PrintPage += PrintPageHandler;
        document.PrinterSettings.PrinterName = printerName;
        document.Print();

        return _printed;
    }

    private void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
        using var g = e.Graphics;

        if (g == null)
        {
            throw new Exception("PrintPageEventArgs contains Graphics with null value");
        }

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