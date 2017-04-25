using System.Collections.Generic;

namespace ProjectEuler
{
    // We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. 
    // For example, 2143 is a 4-digit pandigital and is also prime.
    // Returns the largest n digit pandigital prime that exists.
    internal class Problem41
    {
        public int GetAnswer()
        {
            // Eight and nine digit numbers that are pandigital will always be divisible by 3, so we will look at 7 digit primes.
            const int limit = 7654321;

            List<int> primes = PrimeUtilities.GeneratePrimesUpToN(limit);

            for (int i = primes.Count - 1; i >= 0; --i)
            {
                if (IsPandigital(primes[i].ToString()))
                {
                    return primes[i];
                }
            }

            return -1;
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
