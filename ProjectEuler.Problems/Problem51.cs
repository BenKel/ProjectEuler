using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem51 : ProblemBase
    {
        private const int FamilySize = 9;

        public override string Title => "Prime digit replacements";

        public override string Description => @"
By replacing the 1st digit of the 2-digit number *3, it turns out that six of the nine possible values: 13, 23, 43, 53, 73, and 83, are all prime.

By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number is the first example having seven primes among the ten generated numbers, yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. Consequently 56003, being the first member of this family, is the smallest prime with this property.

Find the smallest prime which, by replacing part of the number (not necessarily adjacent digits) with the same digit, is part of an eight prime value family.
            ";

        public override string GetAnswer()
        {
            const int lowerLimit = 56995; // The example given in the question is 56**3, so that can't be the answer.
            for (int i = lowerLimit; ; i += 2)
            {
                if (!PrimeUtilities.IsPrime(i)) continue;

                if (FitsRule(i))
                {
                    return i.ToString();
                }
            }
        }

        private static bool FitsRule(int number)
        {
            string numberString = number.ToString();

            // First try changing single digits.
            for (int i = 0; i < numberString.Length - 1; ++i)
            {
                if (FamilyIsPrime(numberString, i))
                {
                    return true;
                }
            }

            // Check all of the duplicate numbers.
            foreach (List<int> indexGroup in GetIndicesToReplace(numberString))
            {
                if (FamilyIsPrime(numberString, indexGroup.ToArray()))
                {
                    return true;
                }
            }

            return false;
        }

        // Iterates through all of the family by replacing the digits at the specified indices
        // and replacing them with the digits 0 through 9.
        // Checks if at least 8 members of the family are prime.
        private static bool FamilyIsPrime(string numberString, params int[] replacedIndices)
        {
            int primeCount = 0;

            foreach (var alteredNumber in GetReplacedNumbers(numberString, replacedIndices))
            {
                if (!PrimeUtilities.IsPrime(alteredNumber))
                {
                    continue;
                }

                ++primeCount;

                if (primeCount == FamilySize)
                {
                    return true;
                }
            }

            return false;
        }

        private static IEnumerable<List<int>> GetIndicesToReplace(string numberString)
        {
            // Snippet from stackoverflow to get the indices by creating an object containing each item and its index.
            // Then group by the item and filter out the groups containing more than one object.
            var duplicates = numberString
                .Select((t, i) => new { Index = i, Text = t })
                .GroupBy(g => g.Text)
                .Where(g => g.Count() > 1);

            // Return the indices of each set of duplicates one by one.
            foreach (var group in duplicates)
            {
                List<int> indices = group.Select(i => i.Index).ToList();
                yield return indices;
            }
        }

        private static IEnumerable<int> GetReplacedNumbers(string number, params int[] replacedIndices)
        {
            int insertedNumber = 0;
            // The first digit can't be changed to 0, so skip to 1 if it's included.
            if (replacedIndices.Contains(0))
            {
                insertedNumber = 1;
            }

            for (; insertedNumber <= 9; ++insertedNumber)
            {
                foreach (int index in replacedIndices)
                {
                    number = number.Remove(index, 1);
                    number = number.Insert(index, insertedNumber.ToString());
                }

                yield return int.Parse(number);
            }
        }
    }
}