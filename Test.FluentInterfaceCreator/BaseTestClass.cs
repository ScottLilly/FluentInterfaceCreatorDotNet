using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.ViewModels;

namespace Test.FluentInterfaceCreator;

public abstract class BaseTestClass
{
    protected const string GENERIC_NAMESPACE_NAME = 
        "System.Collections.Generic";

    private readonly OutputLanguage _cSharpLanguage;

    protected BaseTestClass()
    {
        var projectEditor = new ProjectEditor();

        _cSharpLanguage =
            projectEditor.OutputLanguages.First(ol => ol.Name.Matches("C#"));
    }

    protected DataType GetDataTypeWithName(string name)
    {
        return _cSharpLanguage
            .NativeDataTypes
            .First(dt => dt.Name.Matches(name));
    }

    public static IEnumerable<object[]> DataTypeNames()
    {
        yield return new object[] { "void" };
        yield return new object[] { "byte" };
        yield return new object[] { "sbyte" };
        yield return new object[] { "short" };
        yield return new object[] { "ushort" };
        yield return new object[] { "int" };
        yield return new object[] { "uint" };
        yield return new object[] { "long" };
        yield return new object[] { "ulong" };
        yield return new object[] { "float" };
        yield return new object[] { "double" };
        yield return new object[] { "object" };
        yield return new object[] { "char" };
        yield return new object[] { "string" };
        yield return new object[] { "decimal" };
        yield return new object[] { "bool" };
        yield return new object[] { "DateTime" };
    }

    public static IEnumerable<object[]> MethodTypes()
    {
        yield return new object[] { Enums.MethodType.Instantiating };
        yield return new object[] { Enums.MethodType.Chaining };
        yield return new object[] { Enums.MethodType.Executing };
    }
}