namespace FluentInterfaceCreator.Models.Inputs;

public class InterfaceSpec: ITrackChanges
{
    private InterfaceSpecMemento _memento;

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<Guid> CalledByMethodIds { get; set; } = new();
    public List<Guid> CallsIntoMethodIds { get; set; } = new();

    public bool IsDirty =>
        Name != _memento.Name ||
        CalledByMethodIds.OrderBy(i => i)
            .SequenceEqual(_memento.CalledByMethodIds.OrderBy(i => i)) ||
        CallsIntoMethodIds.OrderBy(i => i)
            .SequenceEqual(_memento.CallsIntoMethodIds.OrderBy(i => i));

    public InterfaceSpec()
    {
        SetMementoToCurrentValues();
    }

    public void MarkAsClean()
    {
        SetMementoToCurrentValues();
    }

    private void SetMementoToCurrentValues()
    {
        _memento =
            new InterfaceSpecMemento(Name, CalledByMethodIds, CallsIntoMethodIds);
    }

    private class InterfaceSpecMemento
    {
        public string Name { get; }
        public List<Guid> CalledByMethodIds { get; } = new();
        public List<Guid> CallsIntoMethodIds { get; } = new();

        internal InterfaceSpecMemento(string name, List<Guid> calledByMethodIds,
            List<Guid> callsIntoMethodIds)
        {
            Name = name;
            CalledByMethodIds.AddRange(calledByMethodIds);
            CallsIntoMethodIds.AddRange(callsIntoMethodIds);
        }
    }
}