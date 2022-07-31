using FluentInterfaceCreator.Models.Inputs;

namespace Test.FluentInterfaceCreator.Models.Inputs;

public class TestInterfaceSpec
{
    [Fact]
    public void Test_IsDirty()
    {
        InterfaceSpec spec = new InterfaceSpec();
        Assert.False(spec.IsDirty);

        spec.Name = "ITest";
        Assert.True(spec.IsDirty);

        spec.MarkAsClean();
        Assert.False(spec.IsDirty);

        spec.CalledByMethodIds.Add(Guid.NewGuid());
        Assert.True(spec.IsDirty);

        spec.MarkAsClean();
        Assert.False(spec.IsDirty);

        spec.CallsIntoMethodIds.Add(Guid.NewGuid());
        Assert.True(spec.IsDirty);

        spec.MarkAsClean();
        Assert.False(spec.IsDirty);
    }
}