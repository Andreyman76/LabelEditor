using MySql.Data.MySqlClient;
using System.Text;

namespace MySqlDbApi;

public class MySqlStorage : IPrintingDataSource
{
    private readonly string _connectionString;
    private readonly string _gln = string.Empty;

    public IEnumerable<object> ExampleObjects => new object[]
    {
        new DbData
        {
            Sscc = "00112345678900000019",
            Gtin = 04607013365261,
            Article = "667м",
            Name = "Пломбир шоколадный с бельгийским шоколадом"
        }
    };

    public MySqlStorage(string connectionString)
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
        if (key == null)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Array.Empty<object>();
            }

            yield break;
        }

        if (key is Gtin gtin)
        {
            var serial = GetNextSerialNumber();
            var data = new DbData
            {
                Gtin = gtin.Id,
                Article = gtin.Article,
                Name = gtin.Name,
                Sscc = CreateSsccCode(1, _gln, serial),
            };

            for (int i = 0; i < count; i++)
            {
                yield return new[] { key, data };
            }
        }
        else
        {
            throw new ArgumentException($"Wrong data type as key: {key.GetType()}");
        }

        yield break;
    }

    public IEnumerable<object> GetKeyObjects()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM gtin;";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Gtin()
            {
                Id = reader.GetUInt64("Id"),
                Name = reader.GetString("Name"),
                Article = reader.GetString("Article")
            };
        }
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