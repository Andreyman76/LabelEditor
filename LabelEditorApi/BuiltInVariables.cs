namespace LabelEditorApi;

internal class BuiltInVariables
{
    public DateTime CurrentDateTime { get => DateTime.Now; }
    public string GS { get => "\u001d"; }
    public string Endl { get => "\n"; }
}