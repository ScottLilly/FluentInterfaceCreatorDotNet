using System.Xml;
using FluentInterfaceCreator.Core;
using FluentInterfaceCreator.Models;

namespace FluentInterfaceCreator.Services;

public static class PersistenceService
{
    public static Project GetProjectFromXmlFile(string fileName)
    {
        string serializedProject = File.ReadAllText(fileName);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(serializedProject);

        return Serialization.Deserialize<Project>(serializedProject);
    }

    public static List<OutputLanguage> GetOutputLanguages()
    {
        string json = File.ReadAllText("outputLanguages.json");

        return Newtonsoft.Json
            .JsonConvert.DeserializeObject<List<OutputLanguage>>(json);
    }

    public static void SaveProjectToXmlFile(Project project, string fileName)
    {
        File.WriteAllText(fileName, Serialization.Serialize(project));
    }

    public static void WriteFluentInterfaceFile(FluentInterfaceFile file,
        string outputDirectory)
    {
        File.WriteAllText(Path.Combine(outputDirectory, file.Name),
            file.FormattedText());
    }
}