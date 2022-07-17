using FluentInterfaceCreator.Models.Inputs;

namespace FluentInterfaceCreator.Services;

public static class OutputLanguageRepository
{
    private static readonly List<OutputLanguage>? s_outputLanguages =
        PersistenceService.GetOutputLanguages();

    public static List<OutputLanguage>? GetLanguages()
    {
        return s_outputLanguages;
    }
}