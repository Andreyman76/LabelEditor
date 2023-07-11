using System.ComponentModel;

namespace LabelTemplate;

[Serializable]
[TypeConverter(typeof(PrintingPositionConverter))]
public struct PrintingPosition
{
    [Browsable(true)]
    [Description("Позиция по горизонтали")]
    [DisplayName("X")]
    public float X {  get; set; }

    [Browsable(true)]
    [Description("Позиция по вертикали")]
    [DisplayName("Y")]
    public float Y { get; set; }

    public PrintingPosition()
    {
        X = 0;
        Y = 0;
    }

    public PrintingPosition(float x, float y)
    {
        X = x;
        Y = y;
    }
}