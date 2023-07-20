using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Элемент этикетки Текст
/// </summary>
public class LabelText : LabelElementBase
{
    /// <summary>
    /// Шрифт текста, используемый для XML сериализации/десериализации
    /// </summary>
    [Browsable(false)]
    [XmlElement("Font")]
    public LabelFont LabelFont
    {
        get
        {
            return new()
            {
                Family = Font.FontFamily.Name,
                Size = Font.Size,
                Style = Font.Style
            };
        }

        set
        {
            Font = new(value.Family, value.Size, value.Style);
        }
    }

    /// <summary>
    /// Текст на элементе
    /// </summary>
    [Browsable(true)]
    [Description("Значение текста")]
    [DisplayName("Текст"), Category("Текст")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Шрифт текста
    /// </summary>
    [Browsable(true)]
    [Description("Значение Шрифта")]
    [DisplayName("Шрифт"), Category("Шрифт")]
    [XmlIgnore]
    public Font Font { get; set; } = new("Calibri", 11, FontStyle.Regular);

    /// <summary>
    /// Размер элемента (используется для переноса слов текста в случае превышения размера)
    /// </summary>
    [Browsable(false)]
    [XmlIgnore]
    public PrintingSize Size { get; set; }

    /// <summary>
    /// Поворот
    /// </summary>
    [Browsable(true)]
    [Description("Поворот кода")]
    [DisplayName("Поворот"), Category("Code128")]
    public LabelElementRotationType RotationType { get; set; }

    public override void Draw(Graphics g)
    {
        var format = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        if (RotationType == LabelElementRotationType.Rotate0)
        {
            g.DrawString(Text, Font, Brushes.Black, new RectangleF(Position.X, Position.Y, Size.Width, Size.Height), format);
            return;
        }

        var dotsPerMmX = g.DpiX / 25.4f;
        var dotsPerMmY = g.DpiY / 25.4f;
        var width = dotsPerMmX * Size.Width;
        var height = dotsPerMmY * Size.Height;

        var image = new Bitmap((int)width + 1, (int)height + 1);
        image.SetResolution(g.DpiX, g.DpiY);

        using var graphics = Graphics.FromImage(image);
        graphics.PageUnit = GraphicsUnit.Millimeter;
        graphics.SmoothingMode = SmoothingMode.None;
        graphics.CompositingQuality = CompositingQuality.HighSpeed;
        graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

        graphics.DrawString(Text, Font, Brushes.Black, new RectangleF(0f, 0f, Size.Width, Size.Height), format);
        var x = Position.X;
        var y = Position.Y;

        switch (RotationType)
        {
            case LabelElementRotationType.Rotate90:
                x  -= Size.Height;
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                break;
            case LabelElementRotationType.Rotate180:
                x -= Size.Width;
                y -= Size.Height;
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                break;
            case LabelElementRotationType.Rotate270:
                y -= Size.Width;
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                break;
            default:
                break;
        }
        
        g.DrawImage(image, x, y);
    }

    public override void Replace(string from, string to)
    {
        Text = Text.Replace(from, to);
    }

    public override object Clone()
    {
        return new LabelText()
        {
            Position = Position,
            Font = Font.Clone() as Font ?? throw new Exception("Cloning Font failed"),
            Text = Text,
            Name = Name,
            RotationType = RotationType
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы