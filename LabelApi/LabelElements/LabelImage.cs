using System.ComponentModel;
using System.Drawing;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Элемент этикетки Изображение
/// </summary>
public class LabelImage : LabelElementBase
{
    /// <summary>
    /// Байты изображения
    /// </summary>
    [Browsable(false)]
    public byte[] ImageBytes { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Размер элемента
    /// </summary>
    [Browsable(true)]
    [Description("Размер изображения в мм")]
    [DisplayName("Размер"), Category("Изображение")]
    public PrintingSize Size { get; set; }

    /// <summary>
    /// Поворот
    /// </summary>
    [Browsable(true)]
    [Description("Поворот изображения")]
    [DisplayName("Поворот"), Category("Изображение")]
    public LabelElementRotationType RotationType { get; set; }

    public override void Draw(Graphics g)
    {
        using var stream = new MemoryStream(ImageBytes);
        using var img = new Bitmap(stream);

        switch (RotationType)
        {
            case LabelElementRotationType.Rotate90:
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                break;
            case LabelElementRotationType.Rotate180:
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                break;
            case LabelElementRotationType.Rotate270:
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                break;
            default:
                break;
        }

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
            ImageBytes = ImageBytes.Clone() as byte[] ?? throw new Exception("Cloning ImageBytes failed"),
            Name = Name,
            Position = Position,
            Size = Size,
            RotationType = RotationType
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы