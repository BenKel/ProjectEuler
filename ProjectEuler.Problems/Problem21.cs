using System.Collections.Generic;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem21 : ProblemBase
    {
        public override string Title => "Amicable numbers";
        public override string Description => @"
Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.

For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

Evaluate the sum of all the amicable numbers under 10000.
            ";

        public override string GetAnswer()
        {
            const int limit = 10000;
            // Calculate the sum of amicables as we go.
            // If d(n) > n, ignore it. If d(n) < n, count it. This will avoid duplicates.
            int sumOfAmicables = 0;

            var factorSums = new Dictionary<int, int>();

            for (int i = 2; i < limit; ++i)
            {
                int factorSum = Utility.SumFactors(i);

                if (factorSum >= limit || factorSum == i)
                {
                    continue;
                }

                if (factorSum > i)
                {
                    factorSums.Add(i, factorSum);
                }
                else if (factorSums.ContainsKey(factorSum) && factorSums[factorSum] == i)
                {
                    sumOfAmicables += i;
                    sumOfAmicables += factorSum;
                }
            }

            return sumOfAmicables.ToString();
        }
    }
}