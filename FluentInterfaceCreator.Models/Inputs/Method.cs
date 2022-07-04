using System.Collections.ObjectModel;
using System.ComponentModel;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;

namespace FluentInterfaceCreator.Models.Inputs;

public class Method : INotifyPropertyChanged
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Enums.MethodType Type { get; set; }
    public string Name { get; set; }
    // Only used for Executing methods
    public bool UseIEnumerable { get; set; }
    public DataType? ReturnDataType { get; set; }

    public ObservableCollection<Parameter> Parameters { get; } = new();

    public bool IsValid =>
        Name.IsNotEmpty() &&
        Parameters.All(p => p.IsValid) &&
        ((Type == Enums.MethodType.Executing && ReturnDataType != null) || 
         Type != Enums.MethodType.Executing);

    public bool CanStartChainPair =>
        Type is Enums.MethodType.Instantiating or Enums.MethodType.Chaining;
    public bool CanEndChainPair =>
        Type is Enums.MethodType.Chaining or Enums.MethodType.Executing;
    public bool RequiresReturnDataType =>
        Type == Enums.MethodType.Executing;
    public IEnumerable<string> RequiredNamespaces =>
        Parameters.SelectMany(p => p.RequiredNamespaces)
            .Concat(new List<string>
            {
                ReturnDataType?.ContainingNamespace ?? "",
                UseIEnumerable ? "System.Collections.Generic" : ""
            })
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct();

    public string DataTypeOnlySignature =>
        FormattedReturnDataType + "|" + 
        Name + "=" + 
        string.Join(":", Parameters.Select(p => p.FormattedDataType));
    public string FormattedReturnDataType =>
        Type == Enums.MethodType.Executing
            ? UseIEnumerable
                ? $"IEnumerable<{ReturnDataType?.Name}>" 
                : ReturnDataType?.Name ?? ""
            : "";

    public string FullSignature =>
        $"{Name}({string.Join(", ", Parameters.Select(p => p.FormattedDataTypeAndName))})";

    public event PropertyChangedEventHandler? PropertyChanged;

    public Method(string name, Enums.MethodType type)
    {
        Name = name;
        Type = type;
    }

    // TODO: Find better solution
    // Parameterless constructor for editing ViewModel and desrialization
    public Method()
    {
    }

    // TODO: Find better solution
    // Should only be used for Executing functions
    public Method(string name, Enums.MethodType type, DataType? returnDataType)
    {
        Name = name;
        Type = type;
        ReturnDataType = returnDataType;
    }

    public IEnumerable<string> ValidationErrors()
    {
        if (Type != Enums.MethodType.Instantiating &&
           Type != Enums.MethodType.Chaining &&
           Type != Enums.MethodType.Executing)
        {
            yield return ErrorMessages.GroupIsNotValid;
        }

        if (Type == Enums.MethodType.Executing && ReturnDataType == null)
        {
            yield return ErrorMessages.ReturnTypeIsRequired;
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

    public bool Matches(Method method, bool isCaseSensitive = true) =>
        Name.Matches(method.Name, isCaseSensitive) &&
        FormattedReturnDataType.Matches(method.FormattedReturnDataType, isCaseSensitive) &&
        Parameters.Select(p => p.FormattedDataType)
            .SequenceEqual(method.Parameters.Select(p => p.FormattedDataType));
}