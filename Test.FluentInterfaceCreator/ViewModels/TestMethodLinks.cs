using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.ViewModels;

public class TestMethodLinks : BaseTestClass
{
    [Fact]
    public void Test_AddMethodsAndLinks_RemoveOneMethod()
    {
        var instantiatingMethod = BuildInstantiatingMethod("Create");
        _projectEditor.Project.Methods.Add(instantiatingMethod);

        var chainingMethod1 = BuildChainingMethod("AddNumber");
        chainingMethod1.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod1);

        var chainingMethod2 = BuildChainingMethod("AddBigNumber");
        chainingMethod2.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod2);

        var chainingMethod3 = BuildChainingMethod("AddReallyBigNumber");
        chainingMethod3.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod3);

        var executingMethod = BuildExecutingMethod("ComputeTotal", "int");
        _projectEditor.Project.Methods.Add(executingMethod);

        Assert.Empty(_projectEditor.Project.MethodLinks);

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod1.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod2.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod3.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod1.Id,
            EndingMethodId = executingMethod.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod2.Id,
            EndingMethodId = executingMethod.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod3.Id,
            EndingMethodId = executingMethod.Id
        });

        Assert.Equal(6, _projectEditor.Project.MethodLinks.Count);

        // Remove the second chaining method
        // and confirm its two related MethodLinks are removed
        _projectEditor.Project.Methods.Remove(chainingMethod2);

        Assert.Equal(4, _projectEditor.Project.MethodLinks.Count);
    }

    [Fact]
    public void Test_AddMethodsAndLinks_RemoveTwoMethods()
    {
        var instantiatingMethod = BuildInstantiatingMethod("Create");
        _projectEditor.Project.Methods.Add(instantiatingMethod);

        var chainingMethod1 = BuildChainingMethod("AddNumber");
        chainingMethod1.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod1);

        var chainingMethod2 = BuildChainingMethod("AddBigNumber");
        chainingMethod2.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod2);

        var chainingMethod3 = BuildChainingMethod("AddReallyBigNumber");
        chainingMethod3.Parameters.Add(BuildParameter("value", "int"));
        _projectEditor.Project.Methods.Add(chainingMethod3);

        var executingMethod = BuildExecutingMethod("ComputeTotal", "int");
        _projectEditor.Project.Methods.Add(executingMethod);

        Assert.Empty(_projectEditor.Project.MethodLinks);

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod1.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod2.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = instantiatingMethod.Id,
            EndingMethodId = chainingMethod3.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod1.Id,
            EndingMethodId = executingMethod.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod2.Id,
            EndingMethodId = executingMethod.Id
        });

        _projectEditor.Project.MethodLinks.Add(new MethodLink
        {
            StartingMethodId = chainingMethod3.Id,
            EndingMethodId = executingMethod.Id
        });

        Assert.Equal(6, _projectEditor.Project.MethodLinks.Count);

        // Remove the second and third chaining methods
        // and confirm their four related MethodLinks are removed
        _projectEditor.Project.Methods.Remove(chainingMethod2);
        _projectEditor.Project.Methods.Remove(chainingMethod3);

        Assert.Equal(2, _projectEditor.Project.MethodLinks.Count);
    }
}