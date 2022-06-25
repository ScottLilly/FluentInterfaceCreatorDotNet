using FluentInterfaceCreator.Models.Inputs;

namespace FluentInterfaceCreator.Models;

public static class ExtensionMethods
{
    public static bool IsChainStartingMethod(this Enums.MethodType type)
    {
        return type is Enums.MethodType.Instantiating or Enums.MethodType.Chaining;
    }
}