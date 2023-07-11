using System.Drawing;

namespace LabelTemplate;

public static class LabelGraphicsConvert
{
    public static float PixelsInMillimeter => 3.7795275591f;

    public static float MillimetersToPixels(float mm)
    {
        return mm * PixelsInMillimeter;
    }

    public static Position MillimetersToPixels(Position mm)
    {
        return new(MillimetersToPixels(mm.X), MillimetersToPixels(mm.Y));
    }

    public static float PixelsToMillimeters(float px)
    {
        return px / PixelsInMillimeter;
    }

    public static Position PixelsToMillimeters(Position px)
    {
        return new(PixelsToMillimeters(px.X), PixelsToMillimeters(px.Y));
    }

    public static SizeF MillimetersToPixels(SizeF mm)
    {
        return new(MillimetersToPixels(mm.Width), MillimetersToPixels(mm.Height));
    }

    public static SizeF PixelsToMillimeters(SizeF px)
    {
        return new(PixelsToMillimeters(px.Width), PixelsToMillimeters(px.Height));
    }
}