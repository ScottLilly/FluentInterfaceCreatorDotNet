namespace FluentInterfaceCreator.Models.Inputs;

public class InterfaceSpec: ITrackChanges
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<Guid> CalledByMethodId { get; set; } = new();
    public List<Guid> CallsIntoMethodIds { get; set; } = new();

    public bool IsDirty { get; }

    public void MarkAsClean()
    {
    }
}