using System.ComponentModel;

namespace LabelApi;

/// <summary>
/// Размер элемента на печати в мм
/// </summary>
[Serializable]
[TypeConverter(typeof(PrintingSizeConverter))]
public struct PrintingSize
{
    /// <summary>
    /// Размер по горизонтали
    /// </summary>
    [Browsable(true)]
    [Description("Размер по горизонтали")]
    [DisplayName("Ширина")]
    public float Width { get; set; }

    /// <summary>
    /// Размер по вертикали
    /// </summary>
    [Browsable(true)]
    [Description("Размер по вертикали")]
    [DisplayName("Высота")]
    public float Height { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="width">Размер по горизонтали</param>
    /// <param name="height">Размер по вертикали</param>
    public PrintingSize(float width, float height)
    {
        Width = width;
        Height = height;
    }
}