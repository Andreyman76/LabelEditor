using LabelTemplate;

namespace LabelEditor;

public class UsbPrinter : IPrinter
{
    public string Name { get; init; }
    public Point Dpi { get; set; } = new(203, 203);

    public UsbPrinter(string name)
    {
        Name = name;
    }

    public bool Print(PrinterLabel label)
    {
        if(Name == "toPDF")
        {
            label.PrintToPdf(Name);
            return true;
        }
        else
        {
            var zpl = label.GetZpl(Dpi);
            return RawPrinterHelper.SendStringToPrinter(Name, zpl);
        }
    }

    public void Dispose()
    {
    }
}