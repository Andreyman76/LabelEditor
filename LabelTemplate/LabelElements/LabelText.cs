using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace LabelTemplate;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelText : LabelElementBase
{
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

    [Browsable(true)]
    [Description("Значение текста")]
    [DisplayName("Текст"), Category("Текст")]
    public string Text { get; set; } = string.Empty;

    [Browsable(true)]
    [Description("Значение Шрифта")]
    [DisplayName("Шрифт"), Category("Шрифт")]
    [XmlIgnore]
    public Font Font { get; set; } = new("Calibri", 11, FontStyle.Regular);

    public override void Draw(Graphics g)
    {
        g.DrawString(Text, Font, Brushes.Black, Position.X, Position.Y);
    }

    public override void Replace(string from, string to)
    {
        Text = Text.Replace(from, to);
    }

    public override object Clone()
    {
        return new LabelText
        {
            Position = Position,
            Font = Font.Clone() as Font,
            Text = Text,
            Name = Name
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы