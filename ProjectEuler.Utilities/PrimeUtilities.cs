using System.Collections;
using System.Collections.Generic;

namespace ProjectEuler.Utilities
{
    public static class PrimeUtilities
    {
        public static List<int> GeneratePrimesUpToN(int n)
        {
            BitArray bits = SieveOfEratosthenes(n);
            var primes = new List<int>();
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    primes.Add(i);
                }
            }
            return primes;
        }

        private static BitArray SieveOfEratosthenes(int limit)
        {
            var bits = new BitArray(limit + 1, true)
            {
                [0] = false,
                [1] = false
            };
            for (int i = 0; i * i <= limit; i++)
            {
                if (bits[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        bits[j] = false;
                    }
                }
            }
            return bits;
        }
    }
}