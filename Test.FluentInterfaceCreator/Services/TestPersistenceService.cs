using FluentInterfaceCreator.Services;

namespace Test.FluentInterfaceCreator.Services;

public class TestPersistenceService
{
    [Fact]
    public void Test_LoadProjectFromJSON()
    {
        var project = 
            PersistenceService.GetProjectFromDisk(@".\TestFiles\savedProject.json");

        Assert.NotNull(project);
        Assert.Equal(5, project.InterfaceSpecs.Count);
        Assert.Empty(project.InterfaceSpecs.Where(i => string.IsNullOrWhiteSpace(i.Name)));
        Assert.Equal(17, project.DataTypes.Count(dt => dt.IsNative));
        Assert.Equal(5, project.DataTypes.Count(dt => !dt.IsNative));
    }
}