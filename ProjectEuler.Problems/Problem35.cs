﻿using System.Collections.Generic;
using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Problems
{
    public class Problem35 : ProblemBase
    {
        private readonly IPrimeService _primeService;

        public Problem35(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public override string Title => "Circular primes";

        public override string Description => @"
The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.

There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.

How many circular primes are there below one million?
            ";

        public override string GetAnswer()
        {
            const int limit = 1000000;
            int primeCount = 1;

            for (int i = 3; i < limit; i += 2)
            {
                if (!_primeService.IsPrime(i))
                {
                    continue;
                }

                var perm = new List<char>(i.ToString().ToCharArray());
                bool isCircularPrime = true;

                for (int j = 0; isCircularPrime && j < perm.Count; ++j)
                {
                    if (perm[perm.Count - 1] % 2 == 0)
                    {
                        isCircularPrime = false;
                        break;
                    }

                    if (!_primeService.IsPrime(int.Parse(new string(perm.ToArray()))))
                    {
                        isCircularPrime = false;
                        break;
                    }

                    // Rotate number.
                    for (int k = perm.Count - 2; k >= 0; --k)
                    {
                        char temp = perm[k + 1];
                        perm[k + 1] = perm[k];
                        perm[k] = temp;
                    }
                }

                if (isCircularPrime)
                {
                    ++primeCount;
                }
            }

            return primeCount.ToString();
        }
    }
}