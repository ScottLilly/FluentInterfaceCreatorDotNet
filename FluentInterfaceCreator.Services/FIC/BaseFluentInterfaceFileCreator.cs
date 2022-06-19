using FluentInterfaceCreator.Models;

namespace FluentInterfaceCreator.Services.FIC;

public abstract class BaseFluentInterfaceFileCreator
{
    protected readonly Project _project;

    protected BaseFluentInterfaceFileCreator(Project project)
    {
        _project = project;
    }
}