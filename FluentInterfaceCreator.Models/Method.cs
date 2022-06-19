using System.Collections.ObjectModel;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;
using PropertyChanged;

namespace FluentInterfaceCreator.Models;

[Serializable]
[AddINotifyPropertyChangedInterface]
public class Method
{
    #region Enums

    // TODO: See if we can add descriptions, and get values from resource file
    public enum MethodGroup
    {
        Instantiating,
        Chaining,
        Executing
    }

    // Needed, to bind enum to combobox in WPF UI
    public IEnumerable<MethodGroup> MethodGroups =>
        Enum.GetValues(typeof(MethodGroup)).Cast<MethodGroup>();

    #endregion

    public MethodGroup Group { get; set; }

    public string Name { get; set; }

    // Only used for Executing methods
    public bool RequiresReturnDataType => Group == MethodGroup.Executing;
    public DataType ReturnDataType { get; set; }

    public ObservableCollection<Parameter> Parameters { get; } =
        new ObservableCollection<Parameter>();

    public ObservableCollection<CallableMethodIndicator> MethodsCallableNext { get; } =
        new ObservableCollection<CallableMethodIndicator>();

    #region Derived properties

    public bool IsChainStarting =>
        Group is MethodGroup.Instantiating or MethodGroup.Chaining;
    public bool IsChainEnding =>
        Group is MethodGroup.Chaining or MethodGroup.Executing;

    public string Signature =>
        $"{Name}({ParameterList})";

    public string DataTypeSignature =>
        $"{Name}({ParameterDataTypeList})";

    public string ParameterList =>
        string.Join(", ", Parameters.Select(p => $"{p.DataType.Name} {p.Name}"));

    public string ParameterDataTypeList =>
        string.Join(", ", Parameters.Select(p => $"{p.DataType.Name}"));

    // Used to determine methods that can call the same 'next' methods,
    // to eliminate duplicate InterfaceData objects that identify the same interface.
    public string CallableMethodsSignature =>
        string.Join("|", MethodsCallableNext
            .Where(x => x.CanCall)
            .OrderBy(x => x.Group)
            .ThenBy(x => x.Name)
            .Select(x => $"{x.Group}:{x.Name}"));

    public List<string> NamespacesNeeded
    {
        get
        {
            List<string> namespacesNeeded = new List<string>();

            namespacesNeeded.AddRange(Parameters.Select(parameter => parameter.DataType.ContainingNamespace));
            namespacesNeeded.Add(ReturnDataType?.ContainingNamespace);

            return namespacesNeeded.Where(n => !string.IsNullOrWhiteSpace(n)).Distinct().ToList();
        }
    }

    #endregion

    public Method(MethodGroup group, string name, DataType returnDataType = null)
    {
        Group = group;
        Name = name;
        ReturnDataType = returnDataType;
    }

    // For serialization only
    internal Method()
    {
    }

    public IEnumerable<string> ValidationErrors()
    {
        if(Group != MethodGroup.Instantiating &&
           Group != MethodGroup.Chaining &&
           Group != MethodGroup.Executing)
        {
            yield return ErrorMessages.GroupIsNotValid;
        }

        if(Group == MethodGroup.Executing && ReturnDataType == null)
        {
            yield return ErrorMessages.ReturnTypeIsRequired;
        }

        if(Name.IsEmpty())
        {
            yield return ErrorMessages.NameIsRequired;
        }
        else
        {
            if(Name.HasAnInternalSpace())
            {
                yield return ErrorMessages.NameCannotContainAnInternalSpace;
            }

            if (Name.ContainsInvalidCharacter())
            {
                yield return ErrorMessages.NameCannotContainSpecialCharacters;
            }
        }
    }

    public bool Matches(Method method, bool isCaseSensitive = true)
    {
        StringComparison comparisonMethod = isCaseSensitive
            ? StringComparison.CurrentCulture
            : StringComparison.CurrentCultureIgnoreCase;

        return Name.Equals(method.Name.Trim(), comparisonMethod) &&
               ParameterDataTypeList == method.ParameterDataTypeList;
    }
}