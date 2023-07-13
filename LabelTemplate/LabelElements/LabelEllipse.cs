using System.ComponentModel;
using System.Drawing;

namespace LabelTemplate;

public class LabelEllipse : LabelElementBase
{
    [Browsable(true)]
    [Description("Размер эллипса в мм")]
    [DisplayName("Размер"), Category("Эллипс")]
    public PrintingSize Size { get; set; }

    public override void Draw(Graphics g)
    {
        var pen = new Pen(Color.Black);

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
            Name = Name
        };
    }
}
