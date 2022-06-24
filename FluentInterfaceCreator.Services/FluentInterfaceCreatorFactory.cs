using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Models.Resources;
using FluentInterfaceCreator.Services.FIC;

namespace FluentInterfaceCreator.Services;

public static class FluentInterfaceCreatorFactory
{
    public static IFluentInterfaceCreator GetFluentInterfaceFileCreator(Project project)
    {
        switch(project.OutputLanguage?.Name)
        {
            case "C#":
                return new CSharpFluentInterfaceFileCreator(project);
            default:
                throw new ArgumentException(ErrorMessages.InvalidLanguage,
                    project.OutputLanguage?.Name);
        }
    }
}