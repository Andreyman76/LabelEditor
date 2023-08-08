using MySql.Data.MySqlClient;

namespace MySqlDbApi;

public class GeneralDbStorage : IPrintingDataSource
{
    private readonly string _connectionString;

    public GeneralDbStorage(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<object> ExampleObjects => new object[]
    {
        new GeneralDbData
        {
            Code = "\u001d0101234567891071215jKPuT\u001d93dGVz"
        }
    };

    public void AfterPrint(object[] printedObjects, bool isSuccess)
    {
        if(isSuccess == false)
        {
            return;
        }

        var code = printedObjects.OfType<GeneralDbData>().FirstOrDefault();

        if (code == null)
        {
            return;
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE printer_base SET StatusId = 1 WHERE code = @code;";
        command.Parameters.AddWithValue("code", code.Code);
        command.ExecuteNonQuery();
    }

    public IEnumerable<object> GetKeyObjects()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT printer_base.GtinId, COUNT(*) AS Count, gtin.GtinName FROM printer_base INNER JOIN gtin ON printer_base.GtinId=gtin.GtinId WHERE StatusId = 0 GROUP BY GtinId;";
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var key = new GeneralDbKey
            {
                Gtin = reader.GetUInt64("GtinId"),
                Count = reader.GetInt32("Count"),
                Name = reader.GetString("GtinName")
            };

            yield return key;
        }

        yield break;
    }

    public IEnumerable<object[]> GetLabelDataObjects(object? key, int count)
    {
        if(key == null)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Array.Empty<object>();
            }

            yield break;
        }

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        if(key is GeneralDbKey k)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT Code FROM printer_base WHERE GtinId = @gtin AND StatusId = 0 LIMIT @count;";
            command.Parameters.AddWithValue("gtin", k.Gtin);
            command.Parameters.AddWithValue("count", count);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var code = new GeneralDbData
                {
                    Code = reader.GetString("Code")
                };

                yield return new object[] { code };
            }
        }
        else
        {
            throw new Exception($"{key.GetType()} is not key");
        }
    }
}