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

    public ObservableCollection<Parameter> Parameters { get; } = new();

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

    // Only used for Executing methods
    public bool UseIEnumerable { get; set; }
    public DataType? ReturnDataType { get; set; }
    public string FormattedReturnDataType =>
        UseIEnumerable 
            ? $"IEnumerable<{ReturnDataType?.Name}>" 
            : ReturnDataType?.Name ?? "";

    public event PropertyChangedEventHandler? PropertyChanged;

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