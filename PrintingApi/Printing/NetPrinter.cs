using LabelApi;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace PrintingApi;

public class NetPrinter : IPrinter
{
    public Dpi Dpi { get; set; } = new(203, 203);
    public bool IsConnected => _tcpClient?.Connected ?? false;
    private TcpClient? _tcpClient;
    public IPEndPoint PrinterEndpoint { get; init; }

    private bool _disposed = false;

    public NetPrinter(IPEndPoint printerEndpoint)
    {
        PrinterEndpoint = printerEndpoint;
    }

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