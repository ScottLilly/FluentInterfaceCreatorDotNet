using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestMethod : BaseTestClass
{
    [Fact]
    public void Test_Instantiate()
    {
        var method = new Method();

        Assert.NotNull(method);
    }
}