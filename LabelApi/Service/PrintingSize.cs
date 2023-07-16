using System.ComponentModel;

namespace LabelApi;

/// <summary>
/// Размер элемента на печати в мм
/// </summary>
[Serializable]
[TypeConverter(typeof(PrintingSizeConverter))]
public struct PrintingSize
{
    private float _width = default;
    private float _height = default;

    /// <summary>
    /// Размер по горизонтали
    /// </summary>
    [Browsable(true)]
    [Description("Размер по горизонтали")]
    [DisplayName("Ширина")]
    public float Width
    {
        get => _width;
        set
        {
            if (value <= 0f)
            {
                throw new ArgumentException("Width must be greater than zero");
            }

            _width = value;
        }
    }

    /// <summary>
    /// Размер по вертикали
    /// </summary>
    [Browsable(true)]
    [Description("Размер по вертикали")]
    [DisplayName("Высота")]
    public float Height
    {
        get => _height;
        set
        {
            if (value <= 0f)
            {
                throw new ArgumentException("Height must be greater than zero");
            }

            _height = value;
        }
    }

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