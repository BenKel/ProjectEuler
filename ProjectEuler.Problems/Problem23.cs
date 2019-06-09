using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem23 : ProblemBase
    {
        public override string Title => "Non-abundant sums";

        public override string Description => @"
A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.

A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.

As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.

Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.
            ";

        public override string GetAnswer()
        {
            // Every number above 28123 can be written as the sum of two abundants.
            const int limit = 28123;

            List<int> abundantNumbers = Utility.GetAbundantNumbers(limit);

            var naturalNumbers = new int[limit];

            for (int i = 0; i < naturalNumbers.Length; ++i)
            {
                naturalNumbers[i] = i + 1;
            }

            for (int i = 0; i < abundantNumbers.Count; ++i)
            {
                for (int j = i; abundantNumbers[i] + abundantNumbers[j] <= limit; ++j)
                {
                    naturalNumbers[abundantNumbers[i] + abundantNumbers[j] - 1] = 0;
                }
            }

            int sum = naturalNumbers.Sum();

            return sum.ToString();
        }
    }
}