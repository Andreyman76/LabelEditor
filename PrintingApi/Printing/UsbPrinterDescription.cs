using LabelApi;
using System.ComponentModel;

namespace PrintingApi;

/// <summary>
/// Дескриптор принтера, подключенного по USB
/// </summary>
public class UsbPrinterDescription : IPrinterDescription
{
    [Browsable(true)]
    [Description("Отображаемое имя объекта")]
    [DisplayName("Имя"), Category("Принтер")]
    public string Name { get; set; } = "New Printer";

    /// <summary>
    /// Имя принтера в системе
    /// </summary>
    [ReadOnly(true)]
    [Browsable(true)]
    [Description("Имя принтера в системе")]
    [DisplayName("Имя принтера"), Category("Принтер")]
    public string PrinterName { get; set; } = string.Empty;

    [Browsable(true)]
    [Description("Разрешение печати (точек на дюйм)")]
    [DisplayName("Разрешение"), Category("Принтер")]
    public Dpi Dpi { get; set; } = new(203, 203);

    /// <summary>
    /// Способ обмена данными с принтером
    /// </summary>
    [Browsable(true)]
    [Description("Способ обмена данными с принтером")]
    [DisplayName("Протокол"), Category("Принтер")]
    public UsbPrinterCommunicationType CommunicationType { get; set; } = UsbPrinterCommunicationType.ZPL;

    public SerialazablePrinterDescription GetPrinterDescription()
    {
        return new()
        {
            Name = Name,
            PrinterName = PrinterName,
            Usb = true,
            Dpi = Dpi,
            CommunicationType = CommunicationType
        };
    }

    public IPrinter CreatePrinter()
    {
        var printer = new UsbPrinter(PrinterName)
        {
            Dpi = Dpi,
            CommunicationType = CommunicationType
        };

        return printer;
    }
}