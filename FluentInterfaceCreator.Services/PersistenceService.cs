﻿using FluentInterfaceCreator.Models.Inputs;
using Newtonsoft.Json;

namespace FluentInterfaceCreator.Services;

public static class PersistenceService
{
    public const string FILE_NAME_EXTENSION = ".ficp2";

    public static List<OutputLanguage> GetOutputLanguages()
    {
        string json = File.ReadAllText("outputLanguages.json");

        return JsonConvert.DeserializeObject<List<OutputLanguage>>(json);
    }

    public static void SaveProjectToDisk(Project project, string filename)
    {
        File.WriteAllText(filename,
            JsonConvert.SerializeObject(project, Formatting.Indented));
    }

    public static Project GetProjectFromDisk(string filename)
    {
        var text = File.ReadAllText(filename);
        var project = JsonConvert.DeserializeObject<Project>(text);

        List<InterfaceSpec> autoGeneratedInterfaceSpecs =
            project.InterfaceSpecs
                .Where(i => string.IsNullOrWhiteSpace(i.Name))
                .ToList();

        foreach (var interfaceSpecToRemove in autoGeneratedInterfaceSpecs)
        {
            project.InterfaceSpecs.Remove(interfaceSpecToRemove);
        }

        List<DataType> cleanedDatatypes = new List<DataType>();
        foreach (DataType dataType in project.DataTypes)
        {
            if (!cleanedDatatypes
                    .Exists(dt =>
                        dt.IsNative == dataType.IsNative &&
                        dt.ContainingNamespace == dataType.ContainingNamespace &&
                        dt.Name == dataType.Name))
            {
                cleanedDatatypes.Add(dataType);
            }
        }

        project.DataTypes.Clear();
        foreach (var dataType in cleanedDatatypes)
        {
            project.DataTypes.Add(dataType);
        }

        project.MarkAsClean();

        return project;
    }
}