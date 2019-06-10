using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Utilities
{
    public static class StringUtilities
    {
        // a = 1, b = 2 etc.
        public static int AlphabeticalScore(string input)
        {
            return input.ToUpper().Sum(c => c - 64);
        }

        public static bool HasDuplicateChars(string input)
        {
            var hash = new HashSet<char>();
            foreach (char c in input)
            {
                hash.Add(c);
            }

            return hash.Count != input.Length;
        }

        public static bool IsPalindrome(string input)
        {
            for (int i = 0; i <= input.Length / 2; ++i)
            {
                if (input[i] != input[input.Length - i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsPermutation(string inputOne, string inputTwo)
        {
            if (inputOne.Length != inputTwo.Length)
            {
                return false;
            }

            foreach (char c in inputTwo)
            {
                if (!inputOne.Contains(c))
                {
                    return false;
                }
            }
            foreach (char c in inputOne)
            {
                if (!inputTwo.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}