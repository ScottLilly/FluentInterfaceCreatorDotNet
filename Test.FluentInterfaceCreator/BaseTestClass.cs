using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.ViewModels;

namespace Test.FluentInterfaceCreator;

public abstract class BaseTestClass
{
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
}