namespace FluentInterfaceCreator.Models.Inputs;

public class OutputLanguage
{
    public string Name { get; set; } = "";
    public string FileExtension { get; set; } = "";
    public bool IsCaseSensitive { get; set; } = true;

    public List<DataType> NativeDataTypes { get; } = new();
}