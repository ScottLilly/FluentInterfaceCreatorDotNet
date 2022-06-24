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
    public bool RequiresReturnDataType => Type == Enums.MethodType.Executing;
    public bool UseIEnumerable { get; set; }
    public DataType? ReturnDataType { get; set; }

    public string FormattedReturnDataType =>
        UseIEnumerable ? $"IEnumerable<{ReturnDataType?.Name}>" : ReturnDataType.Name;

    public ObservableCollection<Parameter> Parameters { get; } =
        new ObservableCollection<Parameter>();

    #region Derived properties

    public bool IsChainStarting =>
        Type is Enums.MethodType.Instantiating or Enums.MethodType.Chaining;
    public bool IsChainEnding =>
        Type is Enums.MethodType.Chaining or Enums.MethodType.Executing;

    public string Signature =>
        $"{Name}({ParameterList})";

    public string DataTypeSignature =>
        $"{Name}({ParameterDataTypeList})";

    public string ParameterList =>
        string.Join(", ", Parameters.Select(p => $"{p.FormattedDataType} {p.Name}"));

    public string ParameterDataTypeList =>
        string.Join(", ", Parameters.Select(p => $"{p.FormattedDataType}"));

    public List<string> NamespacesNeeded
    {
        get
        {
            List<string> namespacesNeeded = new List<string>();

            namespacesNeeded.AddRange(Parameters.Select(parameter => parameter.DataType.ContainingNamespace));
            namespacesNeeded.Add(ReturnDataType?.ContainingNamespace);

            if (Parameters.Any(p => p.UseIEnumerable))
            {
                namespacesNeeded.Add("System.Collections.Generic");
            }

            return namespacesNeeded.Where(n => !string.IsNullOrWhiteSpace(n)).Distinct().ToList();
        }
    }

    #endregion

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
        ParameterDataTypeList.Matches(method.ParameterDataTypeList, isCaseSensitive);
}