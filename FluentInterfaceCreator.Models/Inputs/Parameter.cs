using System.ComponentModel;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;
using PropertyChanged;

namespace FluentInterfaceCreator.Models.Inputs;

[SuppressPropertyChangedWarnings]
public class Parameter : INotifyPropertyChanged, ITrackChanges
{
    private ParameterMemento _memento;

    public bool UseIEnumerable { get; set; }
    public DataType? DataType { get; set; }
    public string Name { get; set; } = "";
    public string DefaultValue { get; set; } = "";

    public bool IsDirty =>
        UseIEnumerable != _memento.UseIEnumerable ||
        DataType != _memento.DataType ||
        (DataType?.IsDirty ?? false) ||
        Name != _memento.Name ||
        DefaultValue != _memento.DefaultValue;

    public bool IsValid =>
        Name.IsNotEmpty() &&
        !Name.ContainsInvalidCharacter() &&
        !Name.HasAnInternalSpace() &&
        DataType != null &&
        DataType.IsValid;

    public string FormattedDataType =>
        UseIEnumerable ? $"IEnumerable<{DataType?.Name}>" : DataType?.Name ?? "";
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
        SetMementoToCurrentValues();
    }

    public Parameter(string name, DataType dataType,
        bool useIEnumerable = false)
    {
        Name = name;
        DataType = dataType;
        UseIEnumerable = useIEnumerable;

        SetMementoToCurrentValues();
    }

    public Parameter(string name, DataType dataType,
        string defaultValue)
    {
        Name = name;
        DataType = dataType;
        DefaultValue = defaultValue;

        SetMementoToCurrentValues();
    }

    public Parameter(string name, DataType dataType,
        string defaultValue, bool useIEnumerable)
    {
        Name = name;
        DataType = dataType;
        DefaultValue = defaultValue;
        UseIEnumerable = useIEnumerable;

        SetMementoToCurrentValues();
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
    public void MarkAsClean()
    {
        DataType?.MarkAsClean();

        SetMementoToCurrentValues();
    }

    public bool Matches(Parameter parameter, bool isCaseSensitive = true) =>
        Name.Matches(parameter.Name, isCaseSensitive) &&
        UseIEnumerable == parameter.UseIEnumerable;

    private void SetMementoToCurrentValues()
    {
        _memento = 
            new ParameterMemento(UseIEnumerable, DataType, Name, DefaultValue);
    }

    private class ParameterMemento
    {
        public bool UseIEnumerable { get; set; }
        public DataType? DataType { get; set; }
        public string Name { get; set; } = "";
        public string DefaultValue { get; set; } = "";

        internal ParameterMemento(bool useIEnumerable, DataType? dataType,
            string name, string defaultValue)
        {
            UseIEnumerable = useIEnumerable;
            DataType = dataType;
            Name = name;
            DefaultValue = defaultValue;
        }
    }

}