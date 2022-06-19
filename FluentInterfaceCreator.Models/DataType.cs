using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;

namespace FluentInterfaceCreator.Models;

[Serializable]
public class DataType
{
    public string Name { get; set; }
    public string ContainingNamespace { get; set; }
    public bool IsNative { get; set; }

    #region Constructor(s)

    public static DataType BuildNativeDataType(string name, string containingNamespace)
    {
        return new DataType(name, containingNamespace, true);
    }

    public static DataType BuildCustomDataType(string name, string containingNamespace)
    {
        return new DataType(name, containingNamespace, false);
    }

    private DataType(string name, string containingNamespace, bool isNative)
    {
        Name = name;
        ContainingNamespace = containingNamespace;
        IsNative = isNative;
    }

    // For deserialization
    internal DataType()
    {
    }

    #endregion

    public IEnumerable<string> ValidationErrors()
    {
        if(Name.IsEmpty())
        {
            yield return ErrorMessages.DataTypeIsRequired;
        }
        else
        {
            if(Name.HasAnInternalSpace())
            {
                yield return ErrorMessages.NameCannotContainAnInternalSpace;
            }

            if(Name.ContainsInvalidCharacter())
            {
                yield return ErrorMessages.NameCannotContainSpecialCharacters;
            }
        }

        if(ContainingNamespace.HasAnInternalSpace())
        {
            yield return ErrorMessages.NamespaceCannotContainAnInternalSpace;
        }

        if(ContainingNamespace.IsNotValidNamespace())
        {
            yield return ErrorMessages.NamespaceIsNotValid;
        }
    }

    public bool Matches(DataType dataType, bool isCaseSensitive = true)
    {
        StringComparison comparisonMethod = isCaseSensitive
            ? StringComparison.CurrentCulture
            : StringComparison.CurrentCultureIgnoreCase;

        return Name.Equals(dataType.Name.Trim(), comparisonMethod) &&
               ContainingNamespace.Equals(dataType.ContainingNamespace.Trim(), comparisonMethod);
    }
}