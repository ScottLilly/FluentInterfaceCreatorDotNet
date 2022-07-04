using System.ComponentModel;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;

namespace FluentInterfaceCreator.Models.Inputs;

public class Parameter : INotifyPropertyChanged
{
    public bool UseIEnumerable { get; set; }
    public DataType DataType { get; set; }
    public string Name { get; set; }
    public string DefaultValue { get; set; }

    public string FormattedDataType =>
        UseIEnumerable ? $"IEnumerable<{DataType.Name}>" : DataType.Name;
    public string FormattedDataTypeAndName =>
        DefaultValue.IsEmpty()
            ? $"{FormattedDataType} {Name}"
            : $"{FormattedDataType} {Name} = {DefaultValue}";
    public IEnumerable<string> RequiredNamespaces =>
        new List<string>
        {
            DataType.ContainingNamespace,
            UseIEnumerable ? "System.Collections.Generic" : ""
        }.Where(rn => !string.IsNullOrWhiteSpace(rn));

    public event PropertyChangedEventHandler? PropertyChanged;

    public Parameter()
    {
    }

    public Parameter(string name, DataType dataType, 
        bool useIEnumerable = false)
    {
        Name = name;
        DataType = dataType;
        UseIEnumerable = useIEnumerable;
    }

    public Parameter(string name, DataType dataType, 
        string defaultValue)
    {
        Name = name;
        DataType = dataType;
        DefaultValue = defaultValue;
    }

    public Parameter(string name, DataType dataType, 
        string defaultValue, bool useIEnumerable)
    {
        Name = name;
        DataType = dataType;
        DefaultValue = defaultValue;
        UseIEnumerable = useIEnumerable;
    }

    public IEnumerable<string> ValidationErrors()
    {
        if (DataType == null)
        {
            yield return ErrorMessages.DataTypeIsRequired;
        }

        if (Name.IsEmpty())
        {
            yield return ErrorMessages.NameIsRequired;
        }
        else
        {
            if (Name.HasAnInternalSpace())
            {
                yield return ErrorMessages.NameCannotContainAnInternalSpace;
            }

            if (Name.ContainsInvalidCharacter())
            {
                yield return ErrorMessages.NameCannotContainSpecialCharacters;
            }
        }
    }

    public bool Matches(Parameter parameter, bool isCaseSensitive = true) =>
        Name.Matches(parameter.Name, isCaseSensitive) &&
        UseIEnumerable == parameter.UseIEnumerable;
}