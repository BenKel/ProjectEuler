using System.Linq;
using System.Collections.Generic;

namespace ProjectEuler
{
    internal class Problem51
    {
        private const int FamilySize = 9;

        // Returns the smallest prime which, by replacing part of the number (not necessarily adjacent digits) 
        // with the same digits, is part of an eight prime family.
        public int GetAnswer()
        {
            const int lowerLimit = 56995; // The example given in the question is 56**3, so that can't be the answer.
            for (int i = lowerLimit; ; i += 2)
            {
                if (!PrimeUtilities.IsPrime(i)) continue;

                if (FitsRule(i))
                {
                    return i;
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
                .Select((t, i) => new {Index = i, Text = t})
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
