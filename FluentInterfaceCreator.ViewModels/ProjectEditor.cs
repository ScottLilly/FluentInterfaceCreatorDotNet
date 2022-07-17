using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Services;
using PropertyChanged;

namespace FluentInterfaceCreator.ViewModels;

[AddINotifyPropertyChangedInterface]
public class ProjectEditor
{
    public List<OutputLanguage>? OutputLanguages { get; }

    public Project Project { get; private set; }

    public ProjectEditor()
    {
        OutputLanguages = OutputLanguageRepository.GetLanguages();
        Project = new Project();
    }
}