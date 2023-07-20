using System.ComponentModel;
using System.Drawing;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Элемент этикетки Прямоугольник
/// </summary>
public class LabelRectangle : LabelElementBase
{
    /// <summary>
    /// Размер элемента
    /// </summary>
    [Browsable(true)]
    [Description("Размер прямоугольника в мм")]
    [DisplayName("Размер"), Category("Прямоугольник")]
    public PrintingSize Size { get; set; }

    /// <summary>
    /// Толщина линии
    /// </summary>
    [Browsable(true)]
    [Description("Толщина прямоугольника в мм")]
    [DisplayName("Толщина"), Category("Прямоугольник")]
    public float Width { get; set; } = 1f;

    public override void Draw(Graphics g)
    {
        using var pen = new Pen(Color.Black, Width);
        g.DrawRectangle(pen, new RectangleF(Position.X, Position.Y, Size.Width, Size.Height));
    }

    public override void Replace(string from, string to)
    {
        return;
    }

    public override object Clone()
    {
        return new LabelRectangle
        {
            Position = Position,
            Size = Size,
            Name = Name,
            Width = Width
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы