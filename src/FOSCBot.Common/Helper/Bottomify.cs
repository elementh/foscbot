using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FOSCBot.Common.Helper;

/// <summary>
/// Source: https://github.com/bottom-software-foundation/bottom-dotnet/blob/master/src/Bottom/Bottom.cs
/// </summary>
public class Bottomify
{
    #region Private Attributes

    private const string LINE_ENDING = "👉👈";

    private static readonly Dictionary<byte, string> _character_values = new Dictionary<byte, string>
    {
        {200, "🫂"},
        {50, "💖"},
        {10, "✨"},
        {5, "🥺"},
        {1, ","},
        {0, "❤️"}
    };

    private static readonly Dictionary<string, byte> _character_values_reversed = new Dictionary<string, byte>
    {
        {"🫂", 200},
        {"💖", 50},
        {"✨", 10},
        {"🥺", 5},
        {",", 1},
        {"❤️", 0}
    };

    private static readonly string[] _byte_to_stripped_character_value_group = MapByteToStrippedCharacterValueGroup();
    private static readonly Dictionary<string, byte> _stripped_character_value_group_to_byte = MapStrippedCharacterValueGroupToByte();

    #endregion

    #region Public methods

    /// <summary>
    /// Determines if an input string is a valid Bottom character value group.
    /// </summary>
    /// <param name="input">The string to validate.</param>
    /// <returns>True if the input string is a Bottom character value group, otherwise false.</returns>
    public static bool IsCharacterValueGroup(string input)
    {
        return IsStrippedCharacterValueGroup(StripCharacterValueGroup(input));
    }

    /// <summary>
    /// Encode a Byte in a Bottom character value group.
    /// </summary>
    /// <param name="value">The byte to encode.</param>
    /// <returns>The encoded Bottom character value group.</returns>
    public static string EncodeByte(byte value)
    {
        return _byte_to_stripped_character_value_group[value] + LINE_ENDING;
    }

    /// <summary>
    /// Decode a Bottom character value group into a byte.
    /// </summary>
    /// <param name="input">The Bottom character value group to decode.</param>
    /// <exception cref="KeyNotFoundException">A non-valid bottom character value group was passed.</exception>
    /// <returns>The decoded byte.</returns>
    public static byte DecodeCharacterValueGroup(string input)
    {
        return DecodeStrippedCharacterValueGroup(StripCharacterValueGroup(input));
    }

    /// <summary>
    /// Determines whether a string is Bottom encoded.
    /// </summary>
    /// <param name="input">The string to validate.</param>
    /// <returns>True if the input string is Bottom encoded, otherwise false.</returns>
    public static bool IsEncoded(string input)
    {
        return GetStrippedCharacterValueGroups(input).All(IsStrippedCharacterValueGroup);
    }

    /// <summary>
    /// Encode a string in Bottom.
    /// </summary>
    /// <param name="input">The string to encode.</param>
    /// <returns>The Bottom encoded string.</returns>
    public static string EncodeString(string input)
    {
        return string.Join("", Encoding.UTF8.GetBytes(input).Select(EncodeByte));
    }

    /// <summary>
    /// Decode a Bottom encoded string.
    /// </summary>
    /// <param name="input">The Bottom encoded string to decode.</param>
    /// <exception cref="KeyNotFoundException">The input string contained an invalid Bottom character value group.</exception>
    /// <returns>The decoded string.</returns>
    public static string DecodeString(string input)
    {
        return Encoding.UTF8.GetString(GetStrippedCharacterValueGroups(input).Select(DecodeStrippedCharacterValueGroup).ToArray());
    }

    #endregion

    #region Private Methods
        
    private static bool IsStrippedCharacterValueGroup(string input)
    {
        return GetCodepoints(input).All(s => _character_values_reversed.ContainsKey(s));
    }

    private static byte DecodeStrippedCharacterValueGroup(string input)
    {
        if (_stripped_character_value_group_to_byte.ContainsKey(input))
        {
            return _stripped_character_value_group_to_byte[input];
        }

        if (IsStrippedCharacterValueGroup(input))
        {
            return StrippedCharacterValueGroupToByte(input);
        }
        throw new KeyNotFoundException($"Cannot decode value character \"{input}\".");
    }

    private static string ByteToStrippedCharacterValueGroup(byte value)
    {
        StringBuilder buffer = new StringBuilder();

        do
        {
            foreach (KeyValuePair<byte, string> mapping in _character_values)
            {
                if (value >= mapping.Key)
                {
                    buffer.Append(mapping.Value);
                    value -= mapping.Key;
                    break;
                }
            }
        } while (value > 0);

        return buffer.ToString();
    }

    private static byte StrippedCharacterValueGroupToByte(string input)
    {
        byte value = 0;

        foreach (string characterValue in GetCodepoints(input))
        {
            value += _stripped_character_value_group_to_byte[characterValue];
        }

        _stripped_character_value_group_to_byte[input] = value;
        return value;
    }

    private static IEnumerable<string> GetStrippedCharacterValueGroups(string input)
    {
        return input.Split(new[] { "\u200B", LINE_ENDING }, StringSplitOptions.RemoveEmptyEntries);
    }

    private static IEnumerable<string> GetCodepoints(string input)
    {
        for (int i = 0; i < input.Length; ++i)
        {
            yield return char.ConvertFromUtf32(char.ConvertToUtf32(input, i));
            if (char.IsHighSurrogate(input, i))
            {
                i++;
            }
        }
    }

    private static string StripCharacterValueGroup(string input)
    {
        return Regex.Match(input, @"(.*)(?=\u200B|(?=👉👈))").Value;
    }

    #region Initialiser Methods

    private static string[] MapByteToStrippedCharacterValueGroup()
    {
        string[] mapping = new string[256];

        byte i = 0;
        do
        {
            mapping[i] = ByteToStrippedCharacterValueGroup(i);
            i++;
        } while (i != 0);

        return mapping;
    }

    private static Dictionary<string, byte> MapStrippedCharacterValueGroupToByte()
    {
        Dictionary<string, byte> mapping = new();

        byte i = 0;
        do
        {
            mapping[_byte_to_stripped_character_value_group[i]] = i;
            i++;
        } while (i != 0);

        return mapping;
    }

    #endregion

    #endregion

}