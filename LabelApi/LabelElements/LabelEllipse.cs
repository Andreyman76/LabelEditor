﻿using System.ComponentModel;
using System.Drawing;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Элемент этикетки Эллипс
/// </summary>
public class LabelEllipse : LabelElementBase
{
    /// <summary>
    /// Размер элемента
    /// </summary>
    [Browsable(true)]
    [Description("Размер эллипса в мм")]
    [DisplayName("Размер"), Category("Эллипс")]
    public PrintingSize Size { get; set; }

    /// <summary>
    /// Толщина линии
    /// </summary>
    [Browsable(true)]
    [Description("Толщина эллипса в мм")]
    [DisplayName("Толщина"), Category("Эллипс")]
    public float Width { get; set; } = 1f;

    public override void Draw(Graphics g)
    {
        using var pen = new Pen(Color.Black, Width);
        g.DrawEllipse(pen, new RectangleF(Position.X, Position.Y, Size.Width, Size.Height));
    }

    public override void Replace(string from, string to)
    {
        return;
    }

    public override object Clone()
    {
        return new LabelEllipse
        {
            Position = Position,
            Size = Size,
            Name = Name,
            Width = Width
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы