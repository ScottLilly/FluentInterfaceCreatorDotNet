using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.Models.Inputs;
using Parameter = FluentInterfaceCreator.Models.Inputs.Parameter;

namespace Test.FluentInterfaceCreator.ViewModels;

public class TestProjectEditor : BaseTestClass
{
    [Fact]
    public void Test_Instantiate()
    {
        Assert.Single(_projectEditor.OutputLanguages);
        Assert.Equal(17, _cSharpLanguage?.NativeDataTypes.Count);
    }

    [Fact]
    public void Test_SelectOutputLanguage()
    {
        Assert.Empty(_projectEditor.Project.DataTypes);

        _projectEditor.Project.OutputLanguage = _cSharpLanguage;

        Assert.Equal(17, _projectEditor.Project.DataTypes.Count);
    }

    [Fact]
    public void Test_ProjectCanCreateOutputFiles()
    {
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        _projectEditor.Project.OutputLanguage = _cSharpLanguage;
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        _projectEditor.Project.Name = "FluentEmailBuilder";
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        _projectEditor.Project.NamespaceForFactoryClass = "FluentEmailBuilder";
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        _projectEditor.Project.FactoryClassName = "MailMessageBuilder";
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        var instantiatingMethod = BuildInstantiatingMethod("Create");
        _projectEditor.Project.Methods.Add(instantiatingMethod);
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        var chainingMethod = BuildChainingMethod("AddNumber");
        chainingMethod.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod);
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        var executingMethod = BuildExecutingMethod("ComputeTotal", "int");
        _projectEditor.Project.Methods.Add(executingMethod);
        Assert.False(_projectEditor.Project.CanCreateOutputFiles);

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod.Id,
            EndingMethodId = executingMethod.Id
        });
        Assert.True(_projectEditor.Project.CanCreateOutputFiles);

    }
}