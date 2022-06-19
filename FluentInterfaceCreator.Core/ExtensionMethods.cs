namespace FluentInterfaceCreator.Core;

public static class ExtensionMethods
{
    public static bool IsEmpty(this string text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    public static bool HasText(this string text)
    {
        return !text.IsEmpty();
    }

    public static bool ContainsInvalidCharacter(this string text)
    {
        return !text.All(x => char.IsLetterOrDigit(x) || x.Equals('_') || x.Equals(' '));
    }

    public static bool IsNotValidNamespace(this string text)
    {
        if(text == null || string.IsNullOrWhiteSpace(text))
        {
            return true;
        }

        if(text.Replace(".", "").ContainsInvalidCharacter())
        {
            return true;
        }

        return text.Trim().StartsWith(".") ||
               text.Trim().EndsWith(".") ||
               text.Contains("..");
    }

    public static bool HasAnInternalSpace(this string text)
    {
        return text.HasText() && text.Trim().Contains(' ');
    }

    public static string ToStringWithLineFeeds(this IEnumerable<string> lines)
    {
        return string.Join("\r\n", lines);
    }

    public static string Repeated(this string text, int times)
    {
        return string.Concat(Enumerable.Repeat(text, times));
    }
}