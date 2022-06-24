using System.ComponentModel;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Models.Resources;

namespace FluentInterfaceCreator.Models;

public class InterfaceData : INotifyPropertyChanged
{
    public string Name { get; set; }
    public string CallableMethodsSignature { get; set; }
    public List<Method> CalledByMethods { get; set; } = new List<Method>();
    public List<Method> CallableMethods { get; set; } = new List<Method>();

    public event PropertyChangedEventHandler? PropertyChanged;

    public IEnumerable<string> ValidationErrors()
    {
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

            if(Name.ContainsInvalidCharacter())
            {
                yield return ErrorMessages.NameCannotContainSpecialCharacters;
            }
        }
    }

    public bool Matches(InterfaceData interfaceData, bool isCaseSensitive = true) => 
        Name.Matches(interfaceData.Name, isCaseSensitive);

    public List<string> NamespacesNeeded()
    {
        return CallableMethods.SelectMany(x => x.NamespacesNeeded).Distinct().OrderBy(n => n).ToList();
    }
}