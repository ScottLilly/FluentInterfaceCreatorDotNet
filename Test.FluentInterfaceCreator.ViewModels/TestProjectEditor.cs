using FluentInterfaceCreator.Models;
using FluentInterfaceCreator.ViewModels;

namespace Test.FluentInterfaceCreator.ViewModels
{
    public class TestProjectEditor
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
    }
}