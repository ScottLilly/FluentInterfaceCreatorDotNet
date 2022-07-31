using FluentInterfaceCreator.Models.Inputs;
using FluentInterfaceCreator.Services;
using PropertyChanged;

namespace FluentInterfaceCreator.ViewModels;

[AddINotifyPropertyChangedInterface]
public class ProjectEditor
{
    public List<OutputLanguage>? OutputLanguages { get; }

    public Project? Project { get; private set; }

    public bool HasProject => Project != null;

    public ProjectEditor()
    {
        OutputLanguages = OutputLanguageRepository.GetLanguages();
    }

    public void StartNewProject(OutputLanguage outputLanguage)
    {
        Project = new Project();
        Project.OutputLanguage = outputLanguage;
        Project.MarkAsClean();
    }

    public void LoadProjectFromFile(string fileName)
    {
        Project = PersistenceService.GetProjectFromDisk(fileName);
    }

    public void SaveProjectToFile(string fileName)
    {
        if (Project != null)
        {
            PersistenceService.SaveProjectToDisk(Project, fileName);
        }
    }
}