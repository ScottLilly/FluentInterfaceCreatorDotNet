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
}