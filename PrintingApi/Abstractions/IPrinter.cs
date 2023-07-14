using LabelApi;

namespace PrintingApi;

public interface IPrinter : IDisposable
{
    Dpi Dpi { get; set; }
    bool Print(PrinterLabel label); 
}