using System.Collections.Generic;

namespace ProjectEuler
{
    internal class Problem50
    {
        private static List<int> primes;

        // Returns the prime number p < 1000000 that can be written as the longest sum of consecutive primes.
        public int GetAnswer()
        {
            if (primes == null)
            {
                // The sum of the primes up to 3943 = 1,001,604. No need to go higher.
                primes = PrimeUtilities.GeneratePrimesUpToN(3943);
            }

            // 547 primes under 3943.
            for (int length = 547; length > 0; --length)
            {
                for (int offset = 0; offset <= primes.Count - length; ++offset)
                {
                    int sum = 0;
                    for (int i = 0; i < length; ++i)
                    {
                        sum += primes[offset + i];
                    }

                    if (PrimeUtilities.IsPrime(sum))
                    {
                        return sum;
                    }
                }
            }

            return 0;
        }
    }
}
