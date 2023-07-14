using LabelApi;
using System.ComponentModel;

namespace PrintingApi;

public class UsbPrinterDescription : IPrinterDescription
{
    [Browsable(true)]
    [Description("Отображаемое имя объекта")]
    [DisplayName("Имя"), Category("Принтер")]
    public string Name { get; set; } = "New Printer";

    [ReadOnly(true)]
    [Browsable(true)]
    [Description("Имя принтера в системе")]
    [DisplayName("Имя принтера"), Category("Принтер")]
    public string PrinterName { get; set; } = string.Empty;

    [Browsable(true)]
    [Description("Разрешение печати (точек на дюйм)")]
    [DisplayName("Разрешение"), Category("Принтер")]
    public Dpi Dpi { get; set; } = new(203, 203);

    public PrinterDescription GetPrinterDescription()
    {
        return new()
        {
            Name = Name,
            PrinterName = PrinterName,
            Usb = true,
            Dpi = Dpi
        };
    }

    public IPrinter CreatePrinter()
    {
        var printer = new UsbPrinter(PrinterName)
        {
            Dpi = Dpi
        };

        return printer;
    }
}