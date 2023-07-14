namespace LabelEditor;

public interface IPrinterDescription
{
    string Name { get; set; }
    PrinterDescription GetPrinterDescription();
    IPrinter CreatePrinter();
}