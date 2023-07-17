using LabelApi;
using System.ComponentModel;
using System.Net;
using System.Text.Json.Serialization;

namespace PrintingApi;

/// <summary>
/// Дескриптор принтера, подключенного по сети
/// </summary>
public class NetPrinterDescription : IPrinterDescription
{
    [Browsable(true)]
    [Description("Отображаемое имя объекта")]
    [DisplayName("Имя"), Category("Принтер")]
    public string Name { get; set; } = "New Printer";

    /// <summary>
    /// IP адрес принтера
    /// </summary>
    [Browsable(true)]
    [Description("IP адрес принтера")]
    [DisplayName("IP"), Category("Сеть")]
    [TypeConverter(typeof(IpAddressConverter))]
    public IPAddress Address { get; set; } = IPAddress.Any;

    /// <summary>
    /// Порт принтера
    /// </summary>
    [Browsable(true)]
    [Description("Порт принтера")]
    [DisplayName("Порт"), Category("Сеть")]
    public int Port { get; set; }

    [Browsable(true)]
    [Description("Разрешение печати (точек на дюйм)")]
    [DisplayName("Разрешение"), Category("Принтер")]
    public Dpi Dpi { get; set; } = new(203, 203);

    public SerialazablePrinterDescription GetPrinterDescription()
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