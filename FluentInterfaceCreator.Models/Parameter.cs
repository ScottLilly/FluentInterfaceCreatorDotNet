using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Resources;
using PropertyChanged;

namespace FluentInterfaceCreator.Models;

[Serializable]
[AddINotifyPropertyChangedInterface]
public class Parameter
{
    public DataType DataType { get; set; }
    public string Name { get; set; }

    public IEnumerable<string> ValidationErrors()
    {
        if(DataType == null)
        {
            yield return ErrorMessages.DataTypeIsRequired;
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

    public bool Matches(Parameter parameter, bool isCaseSensitive = true)
    {
        StringComparison comparisonMethod = isCaseSensitive
            ? StringComparison.CurrentCulture
            : StringComparison.CurrentCultureIgnoreCase;

        return Name.Equals(parameter.Name.Trim(), comparisonMethod);
    }
}