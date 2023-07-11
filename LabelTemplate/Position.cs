using System.ComponentModel;

namespace LabelTemplate;

[Serializable]
[TypeConverterAttribute(typeof(PositionConverter))]
public struct Position
{
    public float X {  get; set; }
    public float Y { get; set; }

    public Position()
    {
        X = 0;
        Y = 0;
    }

    public Position(float x, float y)
    {
        X = x;
        Y = y;
    }
}