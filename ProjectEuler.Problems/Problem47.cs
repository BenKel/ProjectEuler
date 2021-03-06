﻿using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Problems
{
    public class Problem47 : ProblemBase
    {
        private readonly IPrimeService _primeService;

        public Problem47(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public override string Title => "Distinct primes factors";

        public override string Description => @"
The first two consecutive numbers to have two distinct prime factors are:

14 = 2 × 7
15 = 3 × 5

The first three consecutive numbers to have three distinct prime factors are:

644 = 2² × 7 × 23
645 = 3 × 5 × 43
646 = 2 × 17 × 19.

Find the first four consecutive integers to have four distinct prime factors each. What is the first of these numbers?
            ";

        public override string GetAnswer()
        {
            var numbers = new Queue<List<int>>();
            numbers.Enqueue(_primeService.GetPrimeFactors(1).ToList());
            numbers.Enqueue(_primeService.GetPrimeFactors(2).ToList());
            numbers.Enqueue(_primeService.GetPrimeFactors(3).ToList());

            for (int i = 4; ; ++i)
            {
                List<int> newFactors = _primeService.GetPrimeFactors(i).ToList();

                CondenseDuplicates(newFactors);

                numbers.Enqueue(newFactors);

                bool found = true;

                foreach (List<int> factors in numbers)
                {
                    if (factors.Count != 4)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }

                numbers.Dequeue();
            }

            List<int> primeFactors = numbers.Dequeue();
            int product = 1;

            foreach (int factor in primeFactors)
            {
                product *= factor;
            }
            return product.ToString();
        }

        private void CondenseDuplicates(List<int> factors)
        {
            // Remove any duplicate prime factors, so 644: 2, 2, 7, 23 would become 644: 4, 7, 23.
            for (int j = factors.Count - 1; j > 0; --j)
            {
                int value = factors[j];
                for (int k = j - 1; k >= 0; --k)
                {
                    if (factors[k] == value)
                    {
                        factors[j] *= factors[k];
                        factors.RemoveAt(k);
                        --j;
                    }
                }
            }
        }
    }
}