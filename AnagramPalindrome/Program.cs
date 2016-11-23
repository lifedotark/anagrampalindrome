using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramPalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new List<string>
            {
                "Eye",
                "Pop",
                "Noon",
                "Level",
                "Radar",
                "Kayak",
                "Rotator",
                "Redivider",
                "Detartrated",
                "Tattarrattat",
                "Aibohphobia",
                "Eve",
                "Bob",
                "Otto",
                "Anna",
                "Hannah",
                "Evilolive",
                "Mirrorrim",
                "Stackcats",
                "Doommood",
                "Risetovotesir",
                "Steponnopets",
                "Neveroddoreven",
                "Anutforajaroftuna",
                "Nolemonnomelon",
                "Somemeninterpretninememos",
                "Gatemanseesnamegaragemanseesnametag",
                "Hannnah"
            }.Select(x => x.ToLower()).ToList();

            var s = new Solution();

            words.ForEach(w => Console.WriteLine($"Text: {w}; Result: {s.solution(w)}"));

            int res = words.Select(w => s.solution(w)).Sum();
            int total = words.Count;

            Console.WriteLine($"All words are valid?: {res == total}");
            Console.ReadKey();
        }
    }
}
