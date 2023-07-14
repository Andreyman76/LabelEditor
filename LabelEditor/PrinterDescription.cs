﻿namespace LabelEditor;

public class PrinterDescription
{
    public string Name { get; set; } = "New Printer";
    public bool Usb { get; set; } = false;
    public string? Address { get; set; }
    public int? Port { get; set; }
    public string? PrinterName { get; set; }
    public Point Dpi { get; set; } = new(203, 203);

    public IPrinterDescription GetPrinterDescription()
    {
        if(Usb == true)
        {
            return new UsbPrinterDescription()
            {
                Name = Name,
                Dpi = Dpi,
                PrinterName = PrinterName
            };
        }
        else
        {
            return new NetPrinterDescription()
            {
                Name = Name,
                Dpi= Dpi,
                Address = Address,
                Port = Port.Value
            };
        }
    }
}