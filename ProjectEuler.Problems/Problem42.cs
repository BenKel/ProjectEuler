using System.Collections.Generic;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem42 : ProblemBase
    {
        private List<int> _triangleNumbers = new List<int>();
        private int _nthTriangleNumberFound;

        public override string Title => "Coded triangle numbers";

        public override string Description => @"
The nth term of the sequence of triangle numbers is given by, tn = ½n(n+1); so the first ten triangle numbers are:

1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...

By converting each letter in a word to a number corresponding to its alphabetical position and adding these values we form a word value. For example, the word value for SKY is 19 + 11 + 25 = 55 = t10. If the word value is a triangle number then we shall call the word a triangle word.

Using a 16K text file containing nearly two-thousand common English words, how many are triangle words?
            ";

        public override string GetAnswer()
        {
            const string filePath = "Data/Problem42Data.txt";
            int count = 0;

            foreach (var name in Utility.YieldParseFileToStringList(filePath))
            {
                int score = StringUtilities.AlphabeticalScore(name);
                if (IsTriangleNumber(score))
                {
                    ++count;
                }
            }

            return count.ToString();
        }

        private bool IsTriangleNumber(int number)
        {
            if (_triangleNumbers.Contains(number))
            {
                return true;
            }

            if (_triangleNumbers.Count > 0 && _triangleNumbers[_triangleNumbers.Count - 1] > number)
            {
                return false;
            }

            ++_nthTriangleNumberFound;
            _triangleNumbers.Add(NthTriangleNumber(_nthTriangleNumberFound));

            return IsTriangleNumber(number);
        }

        private static int NthTriangleNumber(int n)
        {
            return n * (n + 1) / 2;
        }
    }
}