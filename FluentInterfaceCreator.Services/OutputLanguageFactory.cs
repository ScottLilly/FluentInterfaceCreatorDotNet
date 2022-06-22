using FluentInterfaceCreator.Models;

namespace FluentInterfaceCreator.Services;

public static class OutputLanguageFactory
{
    #region Variables with native datatypes for each language

    private static readonly List<OutputLanguage> s_outputLanguages;

    static OutputLanguageFactory()
    {
        s_outputLanguages = PersistenceService.GetOutputLanguages();
    }

    #endregion

    public static List<OutputLanguage> GetLanguages()
    {
        return s_outputLanguages;
    }
}