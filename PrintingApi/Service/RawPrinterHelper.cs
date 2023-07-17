using System.Runtime.InteropServices;
using ZXing;

namespace PrintingApi;

/// <summary>
/// Класс для отправки данных по USB на принтер
/// </summary>
public static class RawPrinterHelper
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal class DocumentInfo
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string DocumentName = string.Empty;
        [MarshalAs(UnmanagedType.LPStr)]
        public string OutputFile = string.Empty;
        [MarshalAs(UnmanagedType.LPStr)]
        public string DataType = string.Empty;
    }

    [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

    [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DocumentInfo di);

    [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    internal static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

    /// <summary>
    /// Отправить байты на принтер
    /// </summary>
    /// <param name="printerName">Имя принтера в системе</param>
    /// <param name="bytes">Данные</param>
    /// <param name="count">Длина данных</param>
    /// <returns>Успешно ли данные были отправлены</returns>
    public static bool SendBytesToPrinter(string printerName, IntPtr bytes, int count)
    {
        var info = new DocumentInfo
        {
            DocumentName = "My C#.NET RAW Document",
            DataType = "RAW"
        };

        var isSuccess = false;
        var written = 0;

        if (OpenPrinter(printerName.Normalize(), out var printer, IntPtr.Zero))
        {
            if (StartDocPrinter(printer, 1, info))
            {
                if (StartPagePrinter(printer))
                {
                    isSuccess = WritePrinter(printer, bytes, count, out written);
                    EndPagePrinter(printer);
                }
                EndDocPrinter(printer);
            }
            ClosePrinter(printer);
        }
        
        return isSuccess && written == count;
    }

    /// <summary>
    /// Отправить файл на принтер
    /// </summary>
    /// <param name="printerName">Имя принтера в системе</param>
    /// <param name="fileName">Путь к файлу</param>
    /// <returns>Успешно ли данные были отправлены</returns>
    public static bool SendFileToPrinter(string printerName, string fileName)
    {
        var fs = new FileStream(fileName, FileMode.Open);
        var br = new BinaryReader(fs);
        var length = Convert.ToInt32(fs.Length);
        var bytes = br.ReadBytes(length);
        var unmanagedBytes = Marshal.AllocCoTaskMem(length);
        Marshal.Copy(bytes, 0, unmanagedBytes, length);
        var isSuccess = SendBytesToPrinter(printerName, unmanagedBytes, length);
        Marshal.FreeCoTaskMem(unmanagedBytes);

        return isSuccess;
    }

    /// <summary>
    /// Отправить строку на принтер
    /// </summary>
    /// <param name="printerName">Имя принтера в системе</param>
    /// <param name="data">Отправляемая строка</param>
    /// <returns>Успешно ли данные были отправлены</returns>
    public static bool SendStringToPrinter(string printerName, string data)
    {
        var length = data.Length;
        var bytes = Marshal.StringToCoTaskMemAnsi(data);
        var isSuccess = SendBytesToPrinter(printerName, bytes, length);
        Marshal.FreeCoTaskMem(bytes);

        return isSuccess;
    }
}