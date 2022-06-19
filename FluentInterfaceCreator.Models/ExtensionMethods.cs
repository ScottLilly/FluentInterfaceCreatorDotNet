namespace FluentInterfaceCreator.Models;

public static class ExtensionMethods
{
    public static bool IsChainStartingMethod(this Method.MethodGroup group)
    {
        return group is Method.MethodGroup.Instantiating or Method.MethodGroup.Chaining;
    }
}