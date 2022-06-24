using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestDataType
{
    [Fact]
    public void Test_Instantiate()
    {
        var dataType = new DataType();

        Assert.NotNull(dataType);
    }
}