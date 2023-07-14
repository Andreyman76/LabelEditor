using LabelApi;

namespace AggregationCodesPrinter;

public interface ILabelDataSource
{
    List<PrinterLabel> GetLabels(int count);
}