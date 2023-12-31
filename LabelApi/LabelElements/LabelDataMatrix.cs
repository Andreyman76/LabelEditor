﻿using SkiaSharp;
using System.ComponentModel;
using System.Drawing;
using ZXing;
using ZXing.Datamatrix;
using ZXing.Datamatrix.Encoder;
using ZXing.SkiaSharp;

namespace LabelApi;
#pragma warning disable CA1416 // Проверка совместимости платформы

/// <summary>
/// Элемент этикетки DataMatrix
/// </summary>
public class LabelDataMatrix : LabelElementBase
{
    /// <summary>
    /// Код
    /// </summary>
    [Browsable(true)]
    [Description("Значение кода")]
    [DisplayName("Код"), Category("DataMatrix")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Размер элемента
    /// </summary>
    [Browsable(true)]
    [Description("Размер кода в мм")]
    [DisplayName("Размер"), Category("DataMatrix")]
    public float Size { get; set; }

    /// <summary>
    /// Поворот
    /// </summary>
    [Browsable(true)]
    [Description("Поворот кода")]
    [DisplayName("Поворот"), Category("DataMatrix")]
    public LabelElementRotationType RotationType { get; set; }

    public override void Draw(Graphics g)
    {
        using var dm = CreateDMImage(Code);

        switch (RotationType)
        {
            case LabelElementRotationType.Rotate90:
                dm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                break;
            case LabelElementRotationType.Rotate180:
                dm.RotateFlip(RotateFlipType.Rotate180FlipNone);
                break;
            case LabelElementRotationType.Rotate270:
                dm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                break;
            default:
                break;
        }

        g.DrawImage(dm, new RectangleF(Position.X, Position.Y, Size, Size));
    }

    public override void Replace(string from, string to)
    {
        Code = Code.Replace(from, to);
    }

    public override object Clone()
    {
        return new LabelDataMatrix
        {
            Position = Position,
            Code = Code,
            Size = Size,
            Name = Name,
            RotationType = RotationType
        };
    }

    /// <summary>
    /// Генерация изображения DataMatrix с помощью ZXing.SkiaSharp
    /// </summary>
    /// <param name="code">Значение кода</param>
    /// <returns>Изображение DataMatrix</returns>
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
}

#pragma warning restore CA1416 // Проверка совместимости платформы