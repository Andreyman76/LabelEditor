using SkiaSharp;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using ZXing;
using ZXing.Datamatrix;
using ZXing.Datamatrix.Encoder;
using ZXing.SkiaSharp;

namespace LabelTemplate;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelDataMatrix : LabelElementBase
{
    [Browsable(true)]
    [Description("Значение кода")]
    [DisplayName("Код"), Category("Datamatrix")]
    public string Code { get; set; } = string.Empty;

    [Browsable(true)]
    [Description("Размер кода")]
    [DisplayName("Размер"), Category("Datamatrix")]
    public int Size { get; set; }

    public override void Draw(Graphics g)
    {
        var positionPx = LabelGraphicsConvert.MillimetersToPixels(Position);
        var sizePx = LabelGraphicsConvert.MillimetersToPixels(Size);
        var dm = CreateDMImage(Code);
        dm.Save("C:/Users/VP/Desktop/dm.png");
        g.SmoothingMode = SmoothingMode.HighSpeed;
        g.CompositingQuality = CompositingQuality.HighSpeed;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.DrawImage(dm, new RectangleF(positionPx.X, positionPx.Y, sizePx, sizePx), new Rectangle(0, 0, dm.Width, dm.Height), GraphicsUnit.Pixel);
    }

    private static Bitmap CreateDMImage(string code)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.DATA_MATRIX,
            Options = new DatamatrixEncodingOptions
            {
                //Height = 56,
                //Width = 56,
                PureBarcode = true,
                SymbolShape = SymbolShapeHint.FORCE_SQUARE,
                GS1Format = true,
                Margin = 0,
            },
        };

        using var bitmap = writer.Write(code);
        using var stream = new MemoryStream();
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
        return new Bitmap(stream);
    }

    public override void BindData(string variableName, string data)
    {
        Code = Code.Replace(variableName, data);
    }

    public override RectangleF GetComputedBounds(Graphics g)
    {
        return new (Position.X, Position.Y, Size, Size);
    }

    public override object Clone()
    {
        return new LabelDataMatrix()
        {
            Position = Position,
            Code = Code,
            Size = Size
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы