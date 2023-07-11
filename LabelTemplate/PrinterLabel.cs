﻿using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Xml.Serialization;

namespace LabelTemplate;
#pragma warning disable CA1416 // Проверка совместимости платформы

[XmlRoot(ElementName = nameof(PrinterLabel))]
public class PrinterLabel : ICloneable
{
    [Browsable(true)]
    [Description("Размер этикетки")]
    [DisplayName("Размер"), Category("Этикетка")]
    public PrintingSize Size { get; set; }

    [Browsable(false)]
    public int Count => Elements.Count;

    [XmlArrayItem(typeof(LabelText))]
    [XmlArrayItem(typeof(LabelImage))]
    [XmlArrayItem(typeof(LabelDataMatrix))]
    [XmlArrayItem(typeof(LabelCode128))]
    [Browsable(false)]
    public List<LabelElementBase> Elements { get; set; } = new();
    private PrintDocument _document = new();

    [Browsable(false)]
    public bool IsReadOnly => false;

    public PrinterLabel()
    {
        _document.PrintPage += PrintPageHandler;
    }

    private void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
        var g = e.Graphics;

        if (g == null)
        {
            throw new Exception("PrintPageEventArgs contains Graphics with null value");
        }

        g.Clear(Color.White);

        g.PageUnit = GraphicsUnit.Millimeter;
        
        foreach (var element in Elements)
        {
            element.Draw(g);
        }
    }

    public Bitmap GetImage()
    {
        var dotsPerMm = 120.0f / 25.4f;
        var width = dotsPerMm * Size.Width;
        var height = dotsPerMm * Size.Height;
        var image = new Bitmap((int)width, (int)height);
        using var g = Graphics.FromImage(image);

        g.PageUnit = GraphicsUnit.Millimeter;
        g.SmoothingMode = SmoothingMode.None;
        g.CompositingQuality = CompositingQuality.HighSpeed;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;

        g.FillRectangle(Brushes.White, new RectangleF(0, 0, Size.Width, Size.Height));

        foreach (var element in Elements)
        {
            element.Draw(g);
        }

        return image;
    }

    public void Print(string printerName)
    {
        _document.PrinterSettings.PrinterName = printerName;
        _document.Print();
    }

    public void Replace(string variableName, string data)
    {
        foreach (var element in Elements)
        {
            element.Replace(variableName, data);
        }
    }

    public object Clone()
    {
        var result = new PrinterLabel();

        foreach (var element in Elements)
        {
            result.Elements.Add(element.Clone() as LabelElementBase);
        }

        return result;
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы