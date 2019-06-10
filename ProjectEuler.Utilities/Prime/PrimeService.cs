using System.Collections.Generic;
using System.Numerics;

namespace ProjectEuler.Utilities.Prime
{
    public class PrimeService : IPrimeService
    {
        private readonly HashSet<long> _cachedPrimes;
        private readonly HashSet<BigInteger> _cachedBigPrimes;

        public PrimeService()
        {
            _cachedPrimes = new HashSet<long>();
            _cachedBigPrimes = new HashSet<BigInteger>();
        }

        public bool IsPrime(long number)
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

        public bool IsPrime(BigInteger number)
        {
            if (number <= long.MaxValue)
            {
                return IsPrime((long)number);
            }

            if (_cachedBigPrimes.Contains(number))
            {
                return true;
            }

            bool isPrime = IsPrimeUncached(number);

            if (isPrime)
            {
                _cachedBigPrimes.Add(number);
            }

            return isPrime;
        }

        public bool IsPrimeUncached(long number)
        {
            if (number == 2)
            {
                return true;
            }

            if (number <= 1 || number % 2 == 0)
            {
                return false;
            }

            for (long i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsPrimeUncached(BigInteger number)
        {
            if (number == 2)
            {
                return true;
            }

            if (number <= 1 || number % 2 == 0)
            {
                return false;
            }

            for (BigInteger i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<int> GetPrimeFactors(long number)
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
                    if (_cachedPrimes.Contains(i) || !IsPrime(i))
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
    }
}