using System.Text.RegularExpressions;

namespace FOSCBot.Core.Common;

public static class StringExtensions
{
    public static bool ContainsWord(this string str, string word)
    {
        return Regex.IsMatch(str, $@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase);
    }

    public static string ReplaceAt(this string input, int index, char newChar)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
        
    public static string ReplaceAt(this string input, int index, string newChars)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        var chars = input.ToCharArray().ToList();
            
        chars.InsertRange(index, newChars);
        //
        // chars.RemoveAt(index);
            
        return new string(chars.ToString());
    }
        
    public static bool IsAllUpper(this string? input)
    {
        // Letterless strings ("2024", "😂😂", "100%") must not count as shouting.
        return !string.IsNullOrEmpty(input) && input.Any(char.IsLetter) &&
               input.All(t => !char.IsLetter(t) || char.IsUpper(t));
    }

    public static bool IsSticker(this string? input)
    {
        return input?.StartsWith("CAAC") ?? false;
    }
        
    public static bool ContainsUrl(this string? input)
    {
        return input is not null && Regex.IsMatch(input, @"\b(?:https?://|www\.)\S+", RegexOptions.IgnoreCase);
    }
}