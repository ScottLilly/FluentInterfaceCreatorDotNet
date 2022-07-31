using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestParameter : BaseTestClass
{
    [Fact]
    public void Test_Instantiate()
    {
        var parameter = new Parameter();
        Assert.NotNull(parameter);
    }

    [Fact]
    public void Test_IsValid()
    {
        var parameter = new Parameter();
        Assert.False(parameter.IsValid);

        parameter.Name = "test";
        Assert.False(parameter.IsValid);

        parameter.DataType = GetDataTypeWithName("string");
        Assert.True(parameter.IsValid);
    }

    [Fact]
    public void Test_NameIsValid()
    {
        var parameter = new Parameter();
        parameter.DataType = GetDataTypeWithName("string");
        Assert.False(parameter.IsValid);

        parameter.Name = "Test Method";
        Assert.False(parameter.IsValid);

        parameter.Name = " TestMethod";
        Assert.False(parameter.IsValid);

        parameter.Name = "TestMethod ";
        Assert.False(parameter.IsValid);

        parameter.Name = "1TestMethod";
        Assert.False(parameter.IsValid);

        parameter.Name = "Test_Method";
        Assert.True(parameter.IsValid);

        parameter.Name = "TestMethod";
        Assert.True(parameter.IsValid);
    }

    [Theory]
    [MemberData(nameof(DataTypeNames))]
    public void Test_NonEnumerableDataType(string dataTypeName)
    {
        var parameter = new Parameter();
        parameter.Name = "value";
        parameter.DataType = GetDataTypeWithName(dataTypeName);

        Assert.Equal(dataTypeName, parameter.FormattedDataType);
    }

    [Theory]
    [MemberData(nameof(DataTypeNames))]
    public void Test_EnumerableDataType(string dataTypeName)
    {
        var parameter = new Parameter();
        parameter.Name = "value";
        parameter.UseIEnumerable = true;
        parameter.DataType = GetDataTypeWithName(dataTypeName);

        Assert.Equal($"IEnumerable<{dataTypeName}>", parameter.FormattedDataType);
    }

    [Fact]
    public void Test_IsDirty()
    {
        Parameter parameter = new Parameter();
        Assert.False(parameter.IsDirty);

        parameter.Name = "Test";
        Assert.True(parameter.IsDirty);

        parameter.MarkAsClean();
        Assert.False(parameter.IsDirty);

        parameter.DataType = GetDataTypeWithName("string");
        Assert.True(parameter.IsDirty);

        parameter.MarkAsClean();
        Assert.False(parameter.IsDirty);

        parameter.DefaultValue = "asd";
        Assert.True(parameter.IsDirty);

        parameter.MarkAsClean();
        Assert.False(parameter.IsDirty);

        parameter.UseIEnumerable = true;
        Assert.True(parameter.IsDirty);

        parameter.MarkAsClean();
        Assert.False(parameter.IsDirty);
    }
}