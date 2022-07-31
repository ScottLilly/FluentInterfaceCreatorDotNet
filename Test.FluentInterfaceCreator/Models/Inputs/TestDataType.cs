using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestDataType : BaseTestClass
{
    [Fact]
    public void Test_Instantiate()
    {
        var dataType = new DataType();

        Assert.NotNull(dataType);
    }

    [Fact] public void Test_IsValid()
    {
        var dataType = new DataType();

        Assert.False(dataType.IsValid);

        dataType.Name = "Test";

        Assert.True(dataType.IsValid);
    }

    [Fact]
    public void Test_NameIsValid()
    {
        var dataType = new DataType();
        Assert.False(dataType.IsValid);

        dataType.Name = "Test Method";
        Assert.False(dataType.IsValid);

        dataType.Name = " TestMethod";
        Assert.False(dataType.IsValid);

        dataType.Name = "TestMethod ";
        Assert.False(dataType.IsValid);

        dataType.Name = "1TestMethod";
        Assert.False(dataType.IsValid);

        dataType.Name = "Test_Method";
        Assert.True(dataType.IsValid);

        dataType.Name = "TestMethod";
        Assert.True(dataType.IsValid);
    }

    [Fact]
    public void Test_IsDirty()
    {
        DataType dataType = new DataType();
        Assert.False(dataType.IsDirty);

        dataType.Name = "Test";
        Assert.True(dataType.IsDirty);

        dataType.MarkAsClean();
        Assert.False(dataType.IsDirty);

        dataType.ContainingNamespace = "TestNamespace";
        Assert.True(dataType.IsDirty);

        dataType.MarkAsClean();
        Assert.False(dataType.IsDirty);

        dataType.IsNative = true;
        Assert.True(dataType.IsDirty);

        dataType.MarkAsClean();
        Assert.False(dataType.IsDirty);
    }
}