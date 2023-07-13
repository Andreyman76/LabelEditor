using LabelTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LabelEditor;

public class ZplPrinter
{
    private TcpClient? _tcpClient;

    public bool IsConnected => _tcpClient?.Connected ?? false;

    public void Connect(IPEndPoint endpoint)
    {
        _tcpClient = new();
        _tcpClient.Connect(endpoint);
    }

    public void Disconnect()
    {
        _tcpClient?.Close();
    }

    public void Print(PrinterLabel label)
    {
        using var image = label.GetImage();
        using var stream = new MemoryStream();

        image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

        var zpl = PDFtoZPL.Conversion.ConvertBitmap(stream.ToArray());
        _tcpClient?.Client.Send(Encoding.UTF8.GetBytes(zpl));
    }
}
