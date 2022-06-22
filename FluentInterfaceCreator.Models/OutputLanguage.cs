using System.Collections.ObjectModel;

namespace FluentInterfaceCreator.Models;

[Serializable]
public class OutputLanguage
{
    public string Name { get; set; }
    public string FileExtension { get; set; }
    public bool IsCaseSensitive { get; set; }

    public ObservableCollection<DataType> NativeDataTypes { get; } =
        new ObservableCollection<DataType>();

    public OutputLanguage(string name, string fileExtension, bool isCaseSensitive,
        IEnumerable<DataType> nativeDataTypes)
    {
        Name = name;
        FileExtension = fileExtension;
        IsCaseSensitive = isCaseSensitive;

        foreach(DataType dataType in nativeDataTypes)
        {
            NativeDataTypes.Add(dataType);
        }
    }

    // For deserialization
    public OutputLanguage()
    {
    }
}