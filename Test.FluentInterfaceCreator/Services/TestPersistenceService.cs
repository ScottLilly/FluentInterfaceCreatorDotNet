using FluentInterfaceCreator.Services;

namespace Test.FluentInterfaceCreator.Services;

public class TestPersistenceService
{
    [Fact]
    public void Test_LoadMSJSON()
    {
        var project = 
            PersistenceService.GetProjectFromDisk(@".\TestFiles\savedProject.json");

        Assert.NotNull(project);
        Assert.Equal(5, project.InterfaceSpecs.Count);
        Assert.Empty(project.InterfaceSpecs.Where(i => string.IsNullOrWhiteSpace(i.Name)));
    }
}