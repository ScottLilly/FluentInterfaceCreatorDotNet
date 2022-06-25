using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestParameter
{
    private List<OutputLanguage> _outputLanguages;

    public TestParameter()
    {
        _outputLanguages = 
    }

    [Fact]
    public void Test_Instantiate()
    {
        var parameter = new Parameter();

        Assert.NotNull(parameter);
    }

    [Fact]
    public void Test_NonEnumerableParameter()
    {
        var parameter = new Parameter();

    }
}