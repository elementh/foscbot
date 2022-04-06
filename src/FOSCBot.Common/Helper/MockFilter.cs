using System.Text;

namespace FOSCBot.Common.Helper;

public class MockFilter
{
    public static string Apply(string original)
    {
        var lastIsUpper = true;
        var stringBuilder = new StringBuilder(original.Length);

        foreach (var character in original)
        {
            if (char.IsLetter(character))
            {
                stringBuilder.Append(lastIsUpper ? char.ToLower(character) : char.ToUpper(character));
                lastIsUpper = !lastIsUpper;
            }
            else
            {
                stringBuilder.Append(character);
            }
        }

        return stringBuilder.ToString();
    }
}