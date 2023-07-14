using LabelApi;
using System.ComponentModel;
using System.Net;

namespace PrintingApi;

public class NetPrinterDescription : IPrinterDescription
{
    [Browsable(true)]
    [Description("Отображаемое имя объекта")]
    [DisplayName("Имя"), Category("Принтер")]
    public string Name { get; set; } = "New Printer";

    [Browsable(true)]
    [Description("IP адрес принтера")]
    [DisplayName("IP"), Category("Принтер")]
    [TypeConverter(typeof(IpAddressConverter))]
    public IPAddress Address { get; set; } = IPAddress.Any;

    [Browsable(true)]
    [Description("Порт принтера")]
    [DisplayName("Порт"), Category("Принтер")]
    public int Port { get; set; }

    [Browsable(true)]
    [Description("Разрешение печати (точек на дюйм)")]
    [DisplayName("Разрешение"), Category("Принтер")]
    public Dpi Dpi { get; set; } = new(203, 203);

    public PrinterDescription GetPrinterDescription()
    {
        return new()
        {
            Name = Name,
            Address = Address.ToString(),
            Port = Port,
            Usb = false,
            Dpi = Dpi
        };
    }

    public IPrinter CreatePrinter()
    {
        var printer = new NetPrinter(new(Address, Port))
        {
            Dpi = Dpi
        };

        return printer;
    }
}