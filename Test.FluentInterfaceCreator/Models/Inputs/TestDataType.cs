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
}