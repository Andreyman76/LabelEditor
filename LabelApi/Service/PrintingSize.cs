using System.ComponentModel;

namespace LabelApi;

[Serializable]
[TypeConverter(typeof(PrintingSizeConverter))]
public struct PrintingSize
{
    private float _width = default;
    private float _height = default;

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

    public PrintingSize()
    {
        Width = 0;
        Height = 0;
    }

    public PrintingSize(float width, float height)
    {
        Width = width;
        Height = height;
    }
}