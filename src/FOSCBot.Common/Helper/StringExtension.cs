using System;
using System.Collections.Generic;
using System.Linq;

namespace FOSCBot.Common.Helper
{
    public static class StringExtension
    {
        public static bool ContainsWord(this string str, string word)
        {
            var lastIndex = 0;
            
            foreach (var character in word)
            {
                if (str.IndexOf(character, lastIndex) == -1)
                {
                    return false;
                }
                
                lastIndex = str.IndexOf(character, lastIndex);
            }

            return true;
        }
        
        public static IEnumerable<int> GetIndexListFor(this string str, string word)
        {
            var indexList = new List<int>();
            
            var lastIndex = 0;
            
            foreach (var character in word)
            {
                if (str.IndexOf(character, lastIndex) == -1)
                {
                    return new int[] {};
                }
                
                lastIndex = str.IndexOf(character, lastIndex);
                
                indexList.Add(lastIndex);
            }

            return indexList;
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
    }
}