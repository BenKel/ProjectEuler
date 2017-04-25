using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace ProjectEuler
{
    internal static class PrimeUtilities
    {
        private static readonly HashSet<long> CachedPrimes;

        static PrimeUtilities()
        {
            CachedPrimes = new HashSet<long> {2, 3, 5, 7};
        }

        public static void AddToCache(List<long> primes)
        {
            foreach (long prime in primes)
            {
                CachedPrimes.Add(prime);
            }
        }

        public static void AddToCache(List<int> primes)
        {
            foreach (int prime in primes)
            {
                CachedPrimes.Add(prime);
            }
        }

        public static bool IsPrimeUncached(long number)
        {
            if (number <= 1 || number % 2 == 0) return false;

            for (long i = 3; i*i <= number; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        public static bool IsPrimeUncached(BigInteger number)
        {
            if (number <= 1 || number % 2 == 0) return false;

            for (BigInteger i = 3; i*i <= number; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        public static bool IsPrime(long number)
        {
            if (CachedPrimes.Contains(number))
            {
                return true;
            }

            bool isPrime = IsPrimeUncached(number);

            if (isPrime)
            {
                CachedPrimes.Add(number);
            }

            return isPrime;
        }

        public static bool IsPrime(BigInteger number)
        {
            if (number <= long.MaxValue)
            {
                return IsPrime((long) number);
            }

            return IsPrimeUncached(number);
        }

        public static List<int> PrimeFactors(long number)
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
                foreach (var prime in CachedPrimes)
                {
                    if (number%prime == 0)
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

                for (int i = 3; i*i <= number; i += 2)
                {
                    if (CachedPrimes.Contains(i))
                    {
                        continue;
                    }

                    if (!IsPrime(i))
                    {
                        continue;
                    }

                    if (number%i == 0)
                    {
                        primeFactors.Add(i);
                        number /= i;
                        break;
                    }
                }
            }
        }

        public static List<BigInteger> PrimeFactors(BigInteger number)
        {
            var primeFactors = new List<BigInteger>();

            if (number == 1)
            {
                primeFactors.Add(1);
                return primeFactors;
            }

            while (true)
            {
                if (IsPrime(number))
                {
                    primeFactors.Add(number);
                    return primeFactors;
                }

                bool modified = false;

                // Use cache first (should be faster due to repeated use)
                foreach (var prime in CachedPrimes)
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

                for (int i = 3; i*i <= number; i += 2)
                {
                    if (CachedPrimes.Contains(i))
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

        // From stackoverflow
        private static int ApproximateNthPrime(int nn)
        {
            double n = nn;
            double p;
            if (nn >= 7022)
            {
                p = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * Math.Log(n) + n * Math.Log(Math.Log(n));
            }
            else if (nn > 0)
            {
                p = new[] { 2, 3, 5, 7, 11 }[nn - 1];
            }
            else
            {
                p = 0;
            }
            return (int)p;
        }

        // From stackoverflow
        // Find all primes up to and including the limit
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

        // From stackoverflow
        public static List<int> GenerateNPrimes(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfEratosthenes(limit);
            var primes = new List<int>();
            for (int i = 0, found = 0; i < limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(i);
                    found++;
                }
            }
            return primes;
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
    }
}
