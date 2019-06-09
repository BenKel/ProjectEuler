using System.Collections.Generic;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem41 : ProblemBase
    {
        public override string Title => "Pandigital prime";

        public override string Description => @"
We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.

What is the largest n-digit pandigital prime that exists?
            ";

        public override string GetAnswer()
        {
            // Eight and nine digit numbers that are pandigital will always be divisible by 3, so we will look at 7 digit primes.
            const int limit = 7654321;

            List<int> primes = PrimeUtilities.GeneratePrimesUpToN(limit);

            for (int i = primes.Count - 1; i >= 0; --i)
            {
                if (IsPandigital(primes[i].ToString()))
                {
                    return primes[i].ToString();
                }
            }

            return null;
        }

        private static bool IsPandigital(string number)
        {
            for (int i = 1; i <= number.Length; ++i)
            {
                if (!number.Contains(i.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}