using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramPalindrome
{
    class Solution
    {
        public int solution(string S)
        {
            if (!S.IsPalindromeCompatible())
                return 0;

            string possiblePalindrome = S.ReorderAsTheSimplestPossiblePalindrome();
            return S.IsAnagramOf(possiblePalindrome) && possiblePalindrome.IsPalindrome() ? 1 : 0;
        }
    }

    internal static class PalindromeExtensions
    {
        public static string ReorderAsTheSimplestPossiblePalindrome(this string palindrome)
        {
            var charsCount =
                palindrome.GetCharCount()
                    .GroupBy(s => s.Count % 2 == 0)
                    .ToDictionary(k => k.Key ? "even" : "odd", v => v.Select(s => s.ToString()).ToList());

            StringBuilder builder = new StringBuilder();
            if (charsCount.ContainsKey("even"))
            {
                builder.Append(charsCount["even"].FirstOrDefault());
                foreach (var charCount in charsCount["even"].Skip(1))
                {
                    builder.Insert(builder.Length / 2, charCount);
                }
            }

            if (charsCount.ContainsKey("odd"))
            {
                builder.Insert(builder.Length / 2,
                    charsCount["odd"].Aggregate(
                        (current, next) => string.Join("", current.Zip(next, (c1, c2) => $"{c1}{c2}"))));
            }

            return builder.ToString();
        }

        public static bool IsPalindromeCompatible(this string text)
        {
            var numberSingleChars = text.GetCharCount().Count(c => c.Count == 1);

            return numberSingleChars <= 1;
        }

        private static IEnumerable<CharCount> GetCharCount(this string text)
        {
            var allChars = text.ToCharArray();
            var uniqueChars = allChars.Distinct();
            var charCount =
                uniqueChars.Select(sc => new CharCount { Char = sc, Count = allChars.Count(ac => ac == sc) })
                    .OrderByDescending(cc => cc.Count % 2 == 0)
                    .ThenBy(cc => cc.Count);
            return charCount;
        }

        public static bool IsPalindrome(this string text)
        {
            string revertedText = string.Join("", text.Reverse());
            return text.Equals(revertedText, StringComparison.OrdinalIgnoreCase);
        }
    }

    internal static class AnagramExtensions
    {
        public static bool IsAnagramOf(this string text, string anagram)
        {
            var textChars = text.ToCharArray().OrderBy(p => p).ToList();
            var anagramChars = anagram.ToCharArray().OrderBy(p => p).ToList();

            return anagramChars.All(c2 => textChars.Contains(c2)) && textChars.Count == anagramChars.Count;
        }
    }

    internal class CharCount
    {
        public char Char { get; set; }
        public int Count { get; set; }

        public IEnumerable<char> AsIEnumerable()
        {
            int count = 0;
            while (count != Count)
            {
                yield return Char;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Char, Count);
            return builder.ToString();
        }
    }
}
