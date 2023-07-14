using System.ComponentModel;
using System.Net;

namespace LabelEditor;

public class NetPrinterDescription : IPrinterDescription
{
    [Browsable(true)]
    [Description("Отображаемое имя объекта")]
    [DisplayName("Имя"), Category("Принтер")]
    public string Name { get; set; } = "New Printer";

    [Browsable(true)]
    [Description("IP адрес принтера")]
    [DisplayName("IP"), Category("Принтер")]
    public string Address { get => _ip.ToString(); set => _ip = IPAddress.Parse(value); }

    [Browsable(true)]
    [Description("Порт принтера")]
    [DisplayName("Порт"), Category("Принтер")]
    public int Port { get; set; }

    [Browsable(true)]
    [Description("Разрешение печати (точек на дюйм)")]
    [DisplayName("Разрешение"), Category("Принтер")]
    public Point Dpi { get; set; } = new(203, 203);

    private IPAddress _ip = IPAddress.Any;

    public PrinterDescription GetPrinterDescription()
    {
        return new()
        {
            Name = Name,
            Address = _ip.ToString(),
            Port = Port,
            Usb = false,
            Dpi = Dpi
        };
    }

    public IPrinter CreatePrinter()
    {
        var printer = new NetPrinter(new(_ip, Port))
        {
            Dpi = Dpi
        };

        return printer;
    }
}