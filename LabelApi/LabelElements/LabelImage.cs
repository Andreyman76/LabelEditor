using System.ComponentModel;
using System.Drawing;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelImage : LabelElementBase
{
    [Browsable(false)]
    public byte[] ImageBytes { get; set; } = Array.Empty<byte>();

    [Browsable(true)]
    [Description("Размер изображения в мм")]
    [DisplayName("Размер"), Category("Изображение")]
    public PrintingSize Size { get; set; }

    public override void Draw(Graphics g)
    {
        using var stream = new MemoryStream(ImageBytes);
        using var img = new Bitmap(stream);
        g.DrawImage(img, new RectangleF(Position.X, Position.Y, Size.Width, Size.Height));
    }

    public override void Replace(string from, string to)
    {
        return;
    }

    public override object Clone()
    {
        return new LabelImage
        {
            ImageBytes = ImageBytes.Clone() as byte[],
            Name = Name,
            Position = Position,
            Size = Size
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы