namespace MappingGenerator.Extensions;

internal static class StringExtension
{
    public static string ToCamelCase(this string text)
    {
        if (!string.IsNullOrEmpty(text) && text.Length > 1)
        {
            return char.ToLowerInvariant(text[0]) + text.Substring(1);
        }
        return text.ToLowerInvariant();
    }
}
