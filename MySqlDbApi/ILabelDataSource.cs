namespace MySqlDbApi;

public interface ILabelDataSource
{
    IEnumerable<object> TestObjects { get; }
    IEnumerable<object> GetSelectableObjects();
    IEnumerable<object[]> GetLabelDataObjects(object? key, int count);
    void OnSuccessPrint(object[] printedObjects);
}