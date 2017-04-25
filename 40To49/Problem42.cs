using System.Collections.Generic;

namespace ProjectEuler
{
    internal class Problem42
    {
        private readonly List<int> triangleNumbers;
        private int nthTriangleNumberFound;

        public Problem42()
        {
            triangleNumbers = new List<int>();
            nthTriangleNumberFound = 0;
        }

        // Returns the number of words in the file Problem42Data that have letter values equal to triangle numbers.
        // A triangle number t(n) is given by t(n) = 0.5n(n+1)
        public int GetAnswer()
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

            return count;
        }

        private bool IsTriangleNumber(int number)
        {
            if (triangleNumbers.Contains(number))
            {
                return true;
            }

            if (triangleNumbers.Count > 0 && triangleNumbers[triangleNumbers.Count - 1] > number)
            {
                return false;
            }

            ++nthTriangleNumberFound;
            triangleNumbers.Add(NthTriangleNumber(nthTriangleNumberFound));

            return IsTriangleNumber(number);
        }

        private static int NthTriangleNumber(int n)
        {
            return n*(n + 1)/2;
        }
    }
}
