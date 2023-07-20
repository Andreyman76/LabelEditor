using LabelApi;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace PrintingApi;

/// <summary>
/// Принтер, подключенный по сети
/// </summary>
public class NetworkPrinter : IPrinter
{
    public Dpi Dpi { get; set; } = new(203, 203);
    public bool IsConnected => _tcpClient?.Connected ?? false;
    private TcpClient? _tcpClient;

    /// <summary>
    /// IP адрес и порт принтера
    /// </summary>
    public IPEndPoint PrinterEndpoint { get; init; }

    private bool _disposed = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="printerEndpoint">IP адрес и порт принтера</param>
    public NetworkPrinter(IPEndPoint printerEndpoint)
    {
        PrinterEndpoint = printerEndpoint;
    }

    /// <summary>
    /// Подключиться к принтеру
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Connect()
    {
        if (IsConnected)
        {
            Disconnect();
        }

        var ping = new Ping();
        var reply = ping.Send(PrinterEndpoint.Address);

        if (reply.Status != IPStatus.Success)
        {
            throw new Exception($"Can't get access to printer: {PrinterEndpoint}");
        }

        _tcpClient = new();
        _tcpClient.Connect(PrinterEndpoint);
    }

    /// <summary>
    /// Отключиться от принтера
    /// </summary>
    public void Disconnect()
    {
        _tcpClient?.Close();
        _tcpClient?.Dispose();
    }

    public bool Print(PrinterLabel label)
    {
        try
        {
            if(IsConnected == false)
            {
                Connect();
            }

            var zpl = label.GetZpl(Dpi);
            _tcpClient?.Client.Send(Encoding.UTF8.GetBytes(zpl));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    protected void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Disconnect();  
        }

        _disposed = true;
    }
}