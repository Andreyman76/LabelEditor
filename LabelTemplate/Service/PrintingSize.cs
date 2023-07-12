using System.ComponentModel;

namespace LabelTemplate;

[Serializable]
[TypeConverter(typeof(PrintingSizeConverter))]
public struct PrintingSize
{
    [Browsable(true)]
    [Description("Размер по горизонтали")]
    [DisplayName("Ширина")]
    public float Width { get; set; }

    [Browsable(true)]
    [Description("Размер по вертикали")]
    [DisplayName("Высота")]
    public float Height { get; set; }

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