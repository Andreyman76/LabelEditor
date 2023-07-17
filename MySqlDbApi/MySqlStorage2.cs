using MySql.Data.MySqlClient;

namespace MySqlDbApi;

public class MySqlStorage2 : IPrintingDataSource
{
    private readonly string _connectionString;

    public MySqlStorage2(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<object> TestObjects => new object[]
    {
        new Gtin()
        {
            Id = 04607013365261,
            Article = "667м",
            Name = "Пломбир шоколадный с бельгийским шоколадом",
        },
        new Pallet()
        {
            Id = 4,
            GtinId = 04607013365261,
            SerialNumber = 0003445,
            Batch = 2,
            Lot = 5,
            Count = 99,
            WarehouseId = 1
        }
    };

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

        var type = key.GetType();

        if (type != typeof(Gtin))
        {
            throw new ArgumentException($"Wrong data type as key: {type}");
        }

        var gtin = key as Gtin ?? throw new Exception($"Convert object to {nameof(Gtin)} failed");
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM pallets WHERE GtinId = @gtin LIMIT @limit;";
        command.Parameters.AddWithValue("gtin", gtin.Id);
        command.Parameters.AddWithValue("limit", count);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var pallet = new Pallet
            {
                Id = reader.GetInt32("Id"),
                GtinId = reader.GetUInt64("GtinId"),
                SerialNumber = reader.GetInt32("SerialNumber"),
                Batch = reader.GetInt32("Batch"),
                Lot = reader.GetInt32("Lot"),
                Count = reader.GetInt32("Count"),
                WarehouseId = reader.GetInt32("WarehouseId")
            };

            yield return new[] { key, pallet };
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

    public void OnSuccessPrint(object[] printedObjects)
    {

    }
}