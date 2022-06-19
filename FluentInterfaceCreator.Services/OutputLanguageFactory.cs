using FluentInterfaceCreator.Models;

namespace FluentInterfaceCreator.Services;

public static class OutputLanguageFactory
{
    #region Variables with native datatypes for each language

    private static readonly List<DataType> s_nativeDataTypesForCSharp =
        new List<DataType>
        {
            DataType.BuildNativeDataType("void", ""),
            DataType.BuildNativeDataType("byte", ""),
            DataType.BuildNativeDataType("sbyte", ""),
            DataType.BuildNativeDataType("short", ""),
            DataType.BuildNativeDataType("ushort", ""),
            DataType.BuildNativeDataType("int", ""),
            DataType.BuildNativeDataType("uint", ""),
            DataType.BuildNativeDataType("long", ""),
            DataType.BuildNativeDataType("ulong", ""),
            DataType.BuildNativeDataType("float", ""),
            DataType.BuildNativeDataType("double", ""),
            DataType.BuildNativeDataType("object", ""),
            DataType.BuildNativeDataType("char", ""),
            DataType.BuildNativeDataType("string", ""),
            DataType.BuildNativeDataType("decimal", ""),
            DataType.BuildNativeDataType("bool", ""),
            DataType.BuildNativeDataType("DateTime", "System")
        };

    #endregion

    public static List<OutputLanguage> GetLanguages()
    {
        return new List<OutputLanguage>
        {
            new OutputLanguage("C#", "cs", true, s_nativeDataTypesForCSharp)
        };
    }
}