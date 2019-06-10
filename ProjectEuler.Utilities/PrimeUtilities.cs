using System.Collections;
using System.Collections.Generic;

namespace ProjectEuler.Utilities
{
    public static class PrimeUtilities
    {
        private static readonly HashSet<long> _cachedPrimes = new HashSet<long> { 2, 3, 5, 7 };

        public static List<int> GetPrimeFactors(long number)
        {
            var primeFactors = new List<int>();

            if (number == 1)
            {
                primeFactors.Add(1);
                return primeFactors;
            }

            while (true)
            {
                if (IsPrime(number))
                {
                    primeFactors.Add((int)number);
                    return primeFactors;
                }

                bool modified = false;

                // Use cache first (should be faster due to repeated use)
                foreach (var prime in _cachedPrimes)
                {
                    if (number % prime == 0)
                    {
                        primeFactors.Add((int)prime);
                        number /= prime;
                        modified = true;
                        break;
                    }
                }

                if (modified)
                {
                    continue;
                }

                for (int i = 3; i * i <= number; i += 2)
                {
                    if (_cachedPrimes.Contains(i))
                    {
                        continue;
                    }

                    if (!IsPrime(i))
                    {
                        continue;
                    }

                    if (number % i == 0)
                    {
                        primeFactors.Add(i);
                        number /= i;
                        break;
                    }
                }
            }
        }

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

        private static bool IsPrime(long number)
        {
            if (_cachedPrimes.Contains(number))
            {
                return true;
            }

            bool isPrime = IsPrimeUncached(number);

            if (isPrime)
            {
                _cachedPrimes.Add(number);
            }

            return isPrime;
        }

        private static bool IsPrimeUncached(long number)
        {
            if (number <= 1 || number % 2 == 0) return false;

            for (long i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
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