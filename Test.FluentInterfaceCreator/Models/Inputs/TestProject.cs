using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestProject : BaseTestClass
{
    [Fact]
    public void Test_Instantiation()
    {
        var project = new Project();

        Assert.NotNull(project);
    }

    [Fact]
    public void Test_IsValid()
    {
        var project = new Project();
        Assert.False(project.IsValid);

        project.Name = "Test";
        Assert.False(project.IsValid);

        project.FactoryClassName = "TestClassName";
        Assert.False(project.IsValid);

        project.NamespaceForFactoryClass = "TestNamespace";
        Assert.False(project.IsValid);

        project.OutputLanguage = GetOutputLanguage();
        Assert.True(project.IsValid);
    }

    [Fact]
    public void Test_IsDirty()
    {
        Project project = new Project();
        Assert.False(project.IsDirty);
    }
}