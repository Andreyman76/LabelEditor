using LabelApi;
using System.Net;
using System.Text.Json.Serialization;

namespace PrintingApi;

/// <summary>
/// Сериализуемый дескриптор принтера
/// </summary>
public class SerialazablePrinterDescription
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
    /// Способ обмена данными с принтером
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UsbPrinterCommunicationType CommunicationType { get; set; }

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
                CommunicationType = CommunicationType
            };
        }
        else
        {
            return new NetworkPrinterDescription()
            {
                Name = Name,
                Dpi= Dpi,
                Address = IPAddress.Parse(Address ?? throw new ArgumentNullException(nameof(Address))),
                Port = Port ?? throw new ArgumentNullException(nameof(Port))
            };
        }
    }
}