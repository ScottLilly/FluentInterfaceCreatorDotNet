namespace FluentInterfaceCreator.Models.Inputs;

public class MethodLink
{
    public Guid StartingMethodId { get; set; }
    public Guid EndingMethodId { get; set; }

    public MethodLink()
    {
    }

    public MethodLink(Guid startingMethodId, Guid endingMethodId)
    {
        StartingMethodId = startingMethodId;
        EndingMethodId = endingMethodId;
    }
}