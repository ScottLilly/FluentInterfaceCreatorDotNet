using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Models.Outputs;

namespace FluentInterfaceCreator.Services.FIC;

internal sealed class CSharpFluentInterfaceFileCreator : 
    IFluentInterfaceCreator
{
    private readonly Project _project;

    public CSharpFluentInterfaceFileCreator(Project project)
    {
        _project = project;
    }

    public FluentInterfaceFile CreateInSingleFile()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FluentInterfaceFile> CreateInMultipleFiles()
    {
        throw new NotImplementedException();
    }
}