namespace MySqlDbApi;

public class Pallet
{
    public int Id { get; set; } = default;
    public ulong GtinId { get; set; } = default;
    public int SerialNumber { get; set; } = default;
    public int Batch { get; set; } = default;
    public int Lot { get; set; } = default;
    public int Count { get; set; } = default;
    public int WarehouseId { get; set; } = default;
}