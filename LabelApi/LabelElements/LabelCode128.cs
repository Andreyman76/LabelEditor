using SkiaSharp;
using System.Drawing;
using ZXing.OneD;
using ZXing;
using ZXing.SkiaSharp;
using System.ComponentModel;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы


/// <summary>
/// Элемент этикетки Code128
/// </summary>
public class LabelCode128 : LabelElementBase
{
    /// <summary>
    /// Код
    /// </summary>
    [Browsable(true)]
    [Description("Значение кода")]
    [DisplayName("Код"), Category("Code128")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Размер элемента
    /// </summary>
    [Browsable(true)]
    [Description("Размер кода в мм")]
    [DisplayName("Размер"), Category("Code128")]
    public PrintingSize Size { get; set; }

    /// <summary>
    /// Использование префикса GS1
    /// </summary>
    [Browsable(true)]
    [Description("Формат GS1")]
    [DisplayName("GS1"), Category("Code128")]
    public bool GS1 { get; set; } = false;

    /// <summary>
    /// Поворот
    /// </summary>
    [Browsable(true)]
    [Description("Поворот кода")]
    [DisplayName("Поворот"), Category("Code128")]
    public LabelElementRotationType RotationType { get; set; }

    public override void Draw(Graphics g)
    {
        using var code128 = CreateCode128Image(Code, GS1);

        switch (RotationType)
        {
            case LabelElementRotationType.Rotate90: 
                code128.RotateFlip(RotateFlipType.Rotate90FlipNone);
                break;
            case LabelElementRotationType.Rotate180:
                code128.RotateFlip(RotateFlipType.Rotate180FlipNone);
                break;
            case LabelElementRotationType.Rotate270: 
                code128.RotateFlip(RotateFlipType.Rotate270FlipNone); 
                break;
            default: 
                break;
        }

        var width = Size.Width;
        var height = Size.Height;

        if (RotationType == LabelElementRotationType.Rotate90 || RotationType == LabelElementRotationType.Rotate270)
        {
            width = Size.Height;
            height = Size.Width;
        }

        g.DrawImage(code128, new RectangleF(Position.X, Position.Y, width, height));
    }

    public override void Replace(string from, string to)
    {
        Code = Code.Replace(from, to);
    }

    public override object Clone()
    {
        return new LabelCode128
        {
            Position = Position,
            Code = Code,
            Size = Size,
            Name = Name,
            GS1 = GS1,
            RotationType = RotationType
        };
    }

    /// <summary>
    /// Генерация изображения Code128 с помощью ZXing.SkiaSharp
    /// </summary>
    /// <param name="code">Значение кода</param>
    /// <param name="gs1">Использование префикса GS1</param>
    /// <returns>Изображение Code128</returns>
    private static Bitmap CreateCode128Image(string code, bool gs1)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.CODE_128,
            Options = new Code128EncodingOptions
            {
                Height = 50,
                PureBarcode = true,
                GS1Format = gs1,
                Margin = 0
            }
        };

        using var bitmap = writer.Write(code);
        using var stream = new MemoryStream();
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
        return new Bitmap(stream);
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы