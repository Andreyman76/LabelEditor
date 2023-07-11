using SkiaSharp;
using System.Drawing;
using ZXing.OneD;
using ZXing;
using ZXing.SkiaSharp;
using System.ComponentModel;

namespace LabelTemplate;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelCode128 : LabelElementBase
{
    [Browsable(true)]
    [Description("Значение кода")]
    [DisplayName("Код"), Category("Code128")]
    public string Code { get; set; } = string.Empty;

    [Browsable(true)]
    [Description("Размер кода")]
    [DisplayName("Размер"), Category("Code128")]
    public SizeF Size { get; set; }

    public override void Draw(Graphics g)
    {
        var positionPx = LabelGraphicsConvert.MillimetersToPixels(Position);
        var sizePx = LabelGraphicsConvert.MillimetersToPixels(Size);

        var code128 = CreateCode128Image(Code, sizePx);
        g.DrawImage(code128, new RectangleF(positionPx.X, positionPx.Y, sizePx.Width, sizePx.Height), new Rectangle(0, 0, code128.Width, code128.Height), GraphicsUnit.Point);
    }

    private static Bitmap CreateCode128Image(string code, SizeF size)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.CODE_128,
            Options = new Code128EncodingOptions
            {
                Height = (int)size.Height,
                Width = (int)size.Width,
                PureBarcode = true,
                GS1Format = true,
                Margin = 0
            },
        };

        using var bitmap = writer.Write(code);
        using var stream = new MemoryStream();
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
        return new Bitmap(stream);
    }

    public override object Clone()
    {
        return new LabelCode128()
        {
            Position = Position,
            Code = Code,
            Size = Size
        };
    }

    public override void BindData(string variableName, string data)
    {
        Code = Code.Replace(variableName, data);
    }

    public override RectangleF GetComputedBounds(Graphics g)
    {
        return new(Position.X, Position.Y, Size.Width, Size.Height);
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы