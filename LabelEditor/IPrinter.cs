using LabelTemplate;

namespace LabelEditor;

public interface IPrinter : IDisposable
{
    Point Dpi { get; set; }
    bool Print(PrinterLabel label); 
}