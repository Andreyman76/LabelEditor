﻿using System.ComponentModel;
using System.Drawing;

namespace LabelTemplate;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelText : LabelElementBase
{
    [Browsable(true)]
    [Description("Семейство шрифта")]
    [DisplayName("Семейство"), Category("Шрифт")]
    public FontFamilies FontFamily { get; set; } = FontFamilies.Calibri;

    [Browsable(true)]
    [Description("Размер шрифта")]
    [DisplayName("Размер"), Category("Шрифт")]
    public int FontSize { get; set; } = 11;

    [Browsable(true)]
    [Description("Стиль шрифта")]
    [DisplayName("Стиль"), Category("Шрифт")]
    public FontStyle FontStyle { get; set; } = FontStyle.Regular;

    [Browsable(true)]
    [Description("Значение текста")]
    [DisplayName("Текст"), Category("Текст")]
    public string Text { get; set; } = string.Empty;

    public override void Draw(Graphics g)
    {
        g.DrawString(Text, new Font(System.Drawing.FontFamily.Families[(int)FontFamily], FontSize, FontStyle), Brushes.Black, Position.X, Position.Y);
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
            FontFamily = FontFamily,
            FontSize = FontSize,
            FontStyle = FontStyle,
            Text = Text
        };
    }

    public override RectangleF GetComputedBounds(Graphics g)
    {
        var size = g.MeasureString(Text, new Font(System.Drawing.FontFamily.Families[(int)FontFamily], FontSize, FontStyle));
        return new(Position.X, Position.Y, (int)size.Width, (int)size.Height);
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы