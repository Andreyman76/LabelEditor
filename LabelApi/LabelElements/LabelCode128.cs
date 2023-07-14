﻿using SkiaSharp;
using System.Drawing;
using ZXing.OneD;
using ZXing;
using ZXing.SkiaSharp;
using System.ComponentModel;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

public class LabelCode128 : LabelElementBase
{
    [Browsable(true)]
    [Description("Значение кода")]
    [DisplayName("Код"), Category("Code128")]
    public string Code { get; set; } = string.Empty;

    [Browsable(true)]
    [Description("Размер кода в мм")]
    [DisplayName("Размер"), Category("Code128")]
    public PrintingSize Size { get; set; }

    [Browsable(true)]
    [Description("Формат GS1")]
    [DisplayName("GS1"), Category("Code128")]
    public bool GS1 { get; set; } = false;

    public override void Draw(Graphics g)
    {
        var code128 = CreateCode128Image(Code, GS1);
        g.DrawImage(code128, new RectangleF(Position.X, Position.Y, Size.Width, Size.Height));
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
            GS1 = GS1
        };
    }

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
            },
        };

        using var bitmap = writer.Write(code);
        using var stream = new MemoryStream();
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
        return new Bitmap(stream);
    }
}

#pragma warning restore CA1416 // Проверка совместимости платформы