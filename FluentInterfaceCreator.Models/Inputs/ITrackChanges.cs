namespace FluentInterfaceCreator.Models.Inputs;

public interface ITrackChanges
{
    bool IsDirty { get; }
    void MarkAsClean();
}