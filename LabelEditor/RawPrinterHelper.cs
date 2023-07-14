using System.Runtime.InteropServices;

public static class RawPrinterHelper
{
    // Structure and API declarions:
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class DocumentInfo
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string DocumentName = string.Empty;
        [MarshalAs(UnmanagedType.LPStr)]
        public string OutputFile = string.Empty;
        [MarshalAs(UnmanagedType.LPStr)]
        public string DataType = string.Empty;
    }

    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DocumentInfo di);

    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

    // SendBytesToPrinter()
    // When the function is given a printer name and an unmanaged array
    // of bytes, the function sends those bytes to the print queue.
    // Returns true on success, false on failure.
    public static bool SendBytesToPrinter(string printerName, IntPtr bytes, Int32 count)
    {
        var written = 0;
        var printer = IntPtr.Zero;

        DocumentInfo info = new DocumentInfo()
        {
            DocumentName = "My C#.NET RAW Document",
            DataType = "RAW"
        };

        var isSuccess = false; // Assume failure unless you specifically succeed.

        // Open the printer.
        if (OpenPrinter(printerName.Normalize(), out printer, IntPtr.Zero))
        {
            // Start a document.
            if (StartDocPrinter(printer, 1, info))
            {
                // Start a page.
                if (StartPagePrinter(printer))
                {
                    // Write your bytes.
                    isSuccess = WritePrinter(printer, bytes, count, out written);
                    EndPagePrinter(printer);
                }
                EndDocPrinter(printer);
            }
            ClosePrinter(printer);
        }

        return isSuccess;
    }

    public static bool SendFileToPrinter(string printerName, string fileName)
    {
        // Open the file.
        var fs = new FileStream(fileName, FileMode.Open);
        // Create a BinaryReader on the file.
        var br = new BinaryReader(fs);
        // Dim an array of bytes big enough to hold the file's contents.
        var bytes = new byte[fs.Length];
        var isSuccess = false;
        // Your unmanaged pointer.

        var length = Convert.ToInt32(fs.Length);
        // Read the contents of the file into the array.
        bytes = br.ReadBytes(length);
        // Allocate some unmanaged memory for those bytes.
        var unmanagedBytes = Marshal.AllocCoTaskMem(length);
        // Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, unmanagedBytes, length);
        // Send the unmanaged bytes to the printer.
        isSuccess = SendBytesToPrinter(printerName, unmanagedBytes, length);
        // Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(unmanagedBytes);
        return isSuccess;
    }

    public static bool SendStringToPrinter(string printerName, string data)
    {
        // How many characters are in the string?
        var length = data.Length;
        // Assume that the printer is expecting ANSI text, and then convert
        // the string to ANSI text.
        var bytes = Marshal.StringToCoTaskMemAnsi(data);
        // Send the converted ANSI string to the printer.
        SendBytesToPrinter(printerName, bytes, length);
        Marshal.FreeCoTaskMem(bytes);
        return true;
    }
}