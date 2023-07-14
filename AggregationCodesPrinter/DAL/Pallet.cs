namespace AggregationCodesPrinter;

public class Pallet
{
    public int Id { get; set; }
    public ulong GtinId { get; set; }
    public int SerialNumber { get; set; }
    public int Batch { get; set; }
    public int Lot { get; set; }
    public int Count { get; set; }
    public int WarehouseId { get; set; }
}