﻿using System.ComponentModel;
using System.Drawing;
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
    /// Размер элемента (используется для переноса слов в текста в случае превышения размера)
    /// </summary>
    [Browsable(false)]
    [XmlIgnore]
    public PrintingSize Size { get; set; } = new(float.MaxValue, float.MaxValue);

    public override void Draw(Graphics g)
    {
        g.DrawString(Text, Font, Brushes.Black, new RectangleF(Position.X, Position.Y, Size.Width, Size.Height), new StringFormat() {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        });
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
            Name = Name
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы