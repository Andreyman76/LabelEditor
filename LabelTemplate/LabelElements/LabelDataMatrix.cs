using SkiaSharp;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
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
    [Description("Размер кода в мм")]
    [DisplayName("Размер"), Category("Datamatrix")]
    public int Size { get; set; }

    public override void Draw(Graphics g)
    {
        var dm = CreateDMImage(Code);
        g.DrawImage(dm, new RectangleF(Position.X, Position.Y, Size, Size));
    }

    private static Bitmap CreateDMImage(string code)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.DATA_MATRIX,
            Options = new DatamatrixEncodingOptions
            {
                PureBarcode = true,
                SymbolShape = SymbolShapeHint.FORCE_SQUARE,
                GS1Format = true,
                Margin = 0,
            }
        };

        using var bitmap = writer.Write(code);
        using var stream = new MemoryStream();
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);

        return new Bitmap(stream);
    }

    public override void Replace(string from, string to)
    {
        Code = Code.Replace(from, to);
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
            Size = Size,
            Name = Name
        };
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы