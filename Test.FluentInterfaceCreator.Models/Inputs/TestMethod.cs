using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestMethod
{
    [Fact]
    public void Test_Instantiate()
    {
        var method = new Method();

        Assert.NotNull(method);
    }
}