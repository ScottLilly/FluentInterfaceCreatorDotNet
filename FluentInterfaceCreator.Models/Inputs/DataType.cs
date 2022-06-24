using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;

namespace FluentInterfaceCreator.Models.Inputs;

public class DataType
{
    public string Name { get; set; } = "";
    public string ContainingNamespace { get; set; } = "";
    public bool IsNative { get; set; } = false;

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
}