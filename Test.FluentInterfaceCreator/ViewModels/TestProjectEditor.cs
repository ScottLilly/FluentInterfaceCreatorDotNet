using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.ViewModels;

namespace Test.FluentInterfaceCreator.ViewModels;

public class TestProjectEditor : BaseTestClass
{
    [Fact]
    public void Test_Instantiate()
    {
        var vm = new ProjectEditor();
        OutputLanguage? cSharpLanguage = 
            vm.OutputLanguages.FirstOrDefault(ol => ol.Name.Equals("C#"));

        Assert.Single(vm.OutputLanguages);
        Assert.Equal(17, cSharpLanguage?.NativeDataTypes.Count);
    }

    [Fact]
    public void Test_SelectOutputLanguage()
    {
        var vm = new ProjectEditor();
        OutputLanguage? cSharpLanguage =
            vm.OutputLanguages.FirstOrDefault(ol => ol.Name.Equals("C#"));

        Assert.Empty(vm.Project.DataTypes);

        vm.Project.OutputLanguage = cSharpLanguage;

        Assert.Equal(17, vm.Project.DataTypes.Count);
    }

    [Fact]
    public void Test_ProjectCanCreateOutputFiles()
    {
        var vm = new ProjectEditor();
        OutputLanguage? cSharpLanguage =
            vm.OutputLanguages.FirstOrDefault(ol => ol.Name.Equals("C#"));

        Assert.False(vm.Project.CanCreateOutputFiles);

        vm.Project.OutputLanguage = cSharpLanguage;
        Assert.False(vm.Project.CanCreateOutputFiles);

        vm.Project.Name = "FluentEmailBuilder";
        Assert.False(vm.Project.CanCreateOutputFiles);

        vm.Project.NamespaceForFactoryClass = "FluentEmailBuilder";
        Assert.False(vm.Project.CanCreateOutputFiles);

        vm.Project.FactoryClassName = "MailMessageBuilder";
        Assert.True(vm.Project.CanCreateOutputFiles);
    }
}