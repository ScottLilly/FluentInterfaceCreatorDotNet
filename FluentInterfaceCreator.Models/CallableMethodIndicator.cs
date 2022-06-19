namespace FluentInterfaceCreator.Models;

[Serializable]
public class CallableMethodIndicator
{
    public Method.MethodGroup Group { get; set; }
    public string Name { get; set; }
    public bool CanCall { get; set; }
    public string Signature { get; set; }
    public string DataTypeSignature { get; set; }

    public CallableMethodIndicator(Method method, bool canCall = false)
    {
        Group = method.Group;
        Name = method.Name;
        Signature = method.Signature;
        DataTypeSignature = method.DataTypeSignature;
        CanCall = canCall;
    }

    // For deserialization
    internal CallableMethodIndicator()
    {
    }
}