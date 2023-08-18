using MySql.Data.MySqlClient;
using System.Text;

namespace MySqlDbApi;

public class VulkanicaStorage : IPrintingDataSource
{
    private readonly string _connectionString;
    private readonly string _gln = string.Empty;

    public IEnumerable<object> ExampleObjects => new object[]
    {
        new VulcanicaDbData
        {
            Sscc = "00112345678900000019"
        }
    };

    public VulkanicaStorage(string connectionString)
    {
        _connectionString = connectionString;

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Gln FROM params";

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            _gln = reader.GetString("Gln");
        }
    }

    public IEnumerable<object[]> GetLabelDataObjects(object? key, int count)
    {
        var serial = GetNextSerialNumber();

        var data = new VulcanicaDbData
        {
            Sscc = CreateSsccCode(1, _gln, serial),
        };

        for (int i = 0; i < count; i++)
        {
            yield return new[] { data };
        }

        yield break;
    }

    public IEnumerable<object> GetKeyObjects()
    {
        return Array.Empty<object>();
    }

    public void AfterPrint(object[] printedObjects, bool isSuccess)
    {

    }

    /// <summary>
    /// Создать SSCC код
    /// </summary>
    /// <param name="extension">Символ расширения - одна цифра</param>
    /// <param name="gln">GLN компании</param>
    /// <param name="serial">Серийний номер</param>
    /// <returns>SSCC код</returns>
    private static string CreateSsccCode(int extension, string gln, ulong serial)
    {
        var sb = new StringBuilder();
        sb.Append(extension);
        sb.Append(gln[0..9]);
        sb.Append(serial.ToString("D7"));

        int sum = 0;
        bool numb = true;
        for (int i = 0; i < sb.Length; i++)
        {
            int a = sb[i] - '0';
            if (numb)
            {
                sum += a * 3;
                numb = false;
            }
            else
            {
                sum += a;
                numb = true;
            }
        }

        sum %= 10;

        if (sum != 0)
        {
            sb.Append((10 - sum));
        }
        else
        {
            sb.Append('0');
        }
        sb.Insert(0, "00");

        return sb.ToString();
    }

    private ulong GetNextSerialNumber()
    {
        var result = 0UL;
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "CALL GetNextSerialNumber();";

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            result = reader.GetUInt64("CurrentSerialNumber");
        }

        return result;
    }
}