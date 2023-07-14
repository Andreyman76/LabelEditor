using System.ComponentModel;

namespace LabelApi;

[Serializable]
[TypeConverter(typeof(DpiConverter))]
public struct Dpi
{
    [Browsable(true)]
    [Description("Горизонтальное разрешение печати")]
    [DisplayName("X")]
    public int X { get; set; }

    [Browsable(true)]
    [Description("Вертикальное разрешение печати")]
    [DisplayName("Y")]
    public int Y { get; set; }

    public Dpi()
    {
        X = 0;
        Y = 0;
    }

    public Dpi(int x, int y)
    {
        X = x;
        Y = y;
    }
}