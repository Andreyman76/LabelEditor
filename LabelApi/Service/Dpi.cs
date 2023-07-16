using System.ComponentModel;

namespace LabelApi;

/// <summary>
/// Разрешение печати в точках на дюйм
/// </summary>
[Serializable]
[TypeConverter(typeof(DpiConverter))]
public struct Dpi
{
    /// <summary>
    /// Горизонтальное разрешение печати
    /// </summary>
    [Browsable(true)]
    [Description("Горизонтальное разрешение печати")]
    [DisplayName("X")]
    public int X { get; set; }

    /// <summary>
    /// Вертикальное разрешение печати
    /// </summary>
    [Browsable(true)]
    [Description("Вертикальное разрешение печати")]
    [DisplayName("Y")]
    public int Y { get; set; }

    public Dpi()
    {
        X = 0;
        Y = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">Горизонтальное разрешение печати</param>
    /// <param name="y">Вертикальное разрешение печати</param>
    public Dpi(int x, int y)
    {
        X = x;
        Y = y;
    }
}