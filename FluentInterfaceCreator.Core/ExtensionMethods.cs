namespace FluentInterfaceCreator.Core;

public static class ExtensionMethods
{
    public static bool IsEmpty(this string text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    public static bool IsNotEmpty(this string text)
    {
        return !text.IsEmpty();
    }

    public static bool ContainsInvalidCharacter(this string text)
    {
        return text.IndexOf(' ') != -1 || 
               !text.All(c => char.IsLetterOrDigit(c) || c == '_') || 
               !char.IsLetter(text[0]);
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
        return text.IsNotEmpty() && text.Trim().Contains(' ');
    }

    public static string ToStringWithLineFeeds(this IEnumerable<string> lines)
    {
        return string.Join("\r\n", lines);
    }

    public static string Repeated(this string text, int times)
    {
        return string.Concat(Enumerable.Repeat(text, times));
    }

    public static bool Matches(this string text, string comparisonText,
        bool isCaseSensitive = true)
    {
        return text.Trim().Equals(comparisonText.Trim(),
            isCaseSensitive 
                ? StringComparison.InvariantCulture 
                : StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool None<T>(this IEnumerable<T> elements, Func<T, bool>? func = null)
    {
        return func == null
            ? !elements.Any()
            : !elements.Any(func.Invoke);
    }
}