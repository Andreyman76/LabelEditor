using MySql.Data.MySqlClient;

namespace MySqlDbApi;

public class MySqlStorage : ILabelDataSource
{
    private string _connectionString;
    public IEnumerable<object> TestObjects => new object[]
    {
        new Gtin()
        {
            Id = 04607013365261,
            Article = "667м",
            Name = "Пломбир шоколадный с бельгийским шоколадом",
        }
    };

    public MySqlStorage(string connectionString)
    {
        _connectionString = connectionString;
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

        var type = key.GetType();

        if (type != typeof(Gtin))
        {
            throw new ArgumentException($"Wrong data type as key: {type}");
        }

        for (int i = 0; i < count; i++)
        {
            yield return new[] { key };
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