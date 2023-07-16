using System.ComponentModel;

namespace LabelApi;

/// <summary>
/// Позиция элемента на печати в мм
/// </summary>
[Serializable]
[TypeConverter(typeof(PrintingPositionConverter))]
public struct PrintingPosition
{
    /// <summary>
    /// Позиция по горизонтали
    /// </summary>
    [Browsable(true)]
    [Description("Позиция по горизонтали")]
    [DisplayName("X")]
    public float X {  get; set; }

    /// <summary>
    /// Позиция по вертикали
    /// </summary>
    [Browsable(true)]
    [Description("Позиция по вертикали")]
    [DisplayName("Y")]
    public float Y { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">Позиция по горизонтали</param>
    /// <param name="y">Позиция по вертикали</param>
    public PrintingPosition(float x, float y)
    {
        X = x;
        Y = y;
    }
}