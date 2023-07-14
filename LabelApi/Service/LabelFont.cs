using System.Drawing;

namespace LabelApi;

public class LabelFont
{
    public string Family { get; set; } = "Calibri";
    public float Size { get; set; } = 11;
    public FontStyle Style { get; set; } = FontStyle.Regular;
}