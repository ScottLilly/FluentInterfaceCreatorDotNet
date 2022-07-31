using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;

namespace FluentInterfaceCreator.Models.Inputs;

public class DataType : ITrackChanges
{
    private DataTypeMemento _memento;

    public bool IsNative { get; set; } = false;
    public string ContainingNamespace { get; set; } = "";
    public string Name { get; set; } = "";

    public bool IsDirty =>
        IsNative != _memento.IsNative ||
        ContainingNamespace != _memento.ContainingNamespace ||
        Name != _memento.Name;

    public bool IsValid =>
        Name.IsNotEmpty() &&
        !Name.ContainsInvalidCharacter() &&
        !Name.HasAnInternalSpace();

    public DataType()
    {
        SetMementoToCurrentValues();
    }

    public void MarkAsClean()
    {
        SetMementoToCurrentValues();
    }

    public IEnumerable<string> ValidationErrors()
    {
        if (Name.IsEmpty())
        {
            yield return ErrorMessages.DataTypeIsRequired;
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

        if (ContainingNamespace.HasAnInternalSpace())
        {
            yield return ErrorMessages.NamespaceCannotContainAnInternalSpace;
        }

        if (ContainingNamespace.IsNotValidNamespace())
        {
            yield return ErrorMessages.NamespaceIsNotValid;
        }
    }

    public bool Matches(DataType dataType, bool isCaseSensitive = true) =>
        Name.Matches(dataType.Name, isCaseSensitive) && 
        ContainingNamespace.Matches(dataType.ContainingNamespace, isCaseSensitive);

    private void SetMementoToCurrentValues()
    {
        _memento =
            new DataTypeMemento(IsNative, ContainingNamespace, Name);
    }

    private class DataTypeMemento
    {
        public bool IsNative { get; set; } = false;
        public string ContainingNamespace { get; set; } = "";
        public string Name { get; set; } = "";

        internal DataTypeMemento(bool isNative, string containingNamespace, string name)
        {
            IsNative = isNative;
            ContainingNamespace = containingNamespace;
            Name = name;
        }
    }

}