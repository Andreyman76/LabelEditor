using LabelApi;

namespace PrintingApi;

/// <summary>
/// Принтер, подключенный по USB
/// </summary>
public class UsbPrinter : IPrinter
{
    /// <summary>
    /// Имя принтера в системе
    /// </summary>
    public string PrinterName { get; init; }
    public Dpi Dpi { get; set; } = new(203, 203);

    /// <summary>
    /// Способ обмена данными с принтером
    /// </summary>
    public UsbPrinterCommunicationType CommunicationType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="printerName">Имя принтера в системе</param>
    public UsbPrinter(string printerName)
    {
        PrinterName = printerName;
    }

    public bool Print(PrinterLabel label)
    {
        if (CommunicationType == UsbPrinterCommunicationType.PDF)
        {
            return label.PrintToPdf(PrinterName); 
        }
        else
        {
            var zpl = label.GetZpl(Dpi);
            return RawPrinterHelper.SendStringToPrinter(PrinterName, zpl);
        }
    }

    public void Dispose()
    {
    }
}