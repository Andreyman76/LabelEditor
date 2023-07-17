using LabelApi;
using System.Net;

namespace PrintingApi;

/// <summary>
/// Сериализуемый дескриптор принтера
/// </summary>
public class SerialaziblePrinterDescription
{
    /// <summary>
    /// Отображаемое имя принтера
    /// </summary>
    public string Name { get; set; } = "New Printer";

    /// <summary>
    /// Подключен ли принтер по USB
    /// </summary>
    public bool Usb { get; set; } = false;

    /// <summary>
    /// IP адрес принтера
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Порт принтера
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// Системное имя принтера
    /// </summary>
    public string? PrinterName { get; set; }

    /// <summary>
    /// Разрешение печати принтера
    /// </summary>
    public Dpi Dpi { get; set; } = new(203, 203);

    /// <summary>
    /// Получить дескриптор принтера
    /// </summary>
    /// <returns>Дескриптор принтера</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IPrinterDescription GetPrinterDescription()
    {
        if(Usb == true)
        {
            return new UsbPrinterDescription()
            {
                Name = Name,
                Dpi = Dpi,
                PrinterName = PrinterName ?? throw new ArgumentNullException(nameof(PrinterName)),
            };
        }
        else
        {
            return new NetPrinterDescription()
            {
                Name = Name,
                Dpi= Dpi,
                Address = IPAddress.Parse(Address ?? throw new ArgumentNullException(nameof(Address))),
                Port = Port ?? throw new ArgumentNullException(nameof(Port))
            };
        }
    }
}