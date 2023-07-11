using System.ComponentModel;
using System.Drawing;

namespace LabelTemplate;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelImage : LabelElementBase
{
    public byte[] ImageBytes { get; set; } = Array.Empty<byte>();

    [Browsable(true)]
    [Description("Размер изображения")]
    [DisplayName("Размер"), Category("Изображение")]
    public SizeF Size { get; set; }

    public override void Draw(Graphics g)
    {
        var positionPx = LabelGraphicsConvert.MillimetersToPixels(Position);
        var sizePx = LabelGraphicsConvert.MillimetersToPixels(Size);

        using var stream = new MemoryStream(ImageBytes);
        using var img = new Bitmap(stream);
        g.DrawImage(img, positionPx.X, positionPx.Y, sizePx.Width, sizePx.Height);
    }

    public override void BindData(string variableName, string data)
    {
        return;
    }

    public override object Clone()
    {
        return new LabelImage()
        {
            ImageBytes = ImageBytes.Clone() as byte[],
        };
    }

    public override RectangleF GetComputedBounds(Graphics g)
    {
        return new(Position.X, Position.Y, Size.Width, Size.Height);
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы