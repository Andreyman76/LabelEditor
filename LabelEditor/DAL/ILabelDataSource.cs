using LabelTemplate;

namespace LabelEditor;

public interface ILabelDataSource
{
    List<PrinterLabel> GetLabels(int count);
}