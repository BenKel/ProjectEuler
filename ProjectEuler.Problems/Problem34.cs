﻿using System.Linq;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    // TODO: quicken this. It's slow. Could use a cache for smaller numbers already calculated.
    public class Problem34 : ProblemBase
    {
        private readonly int[] _factorialCache =
        {
            1,
            1,
            2,
            (int)Utility.NFactorial(3),
            (int)Utility.NFactorial(4),
            (int)Utility.NFactorial(5),
            (int)Utility.NFactorial(6),
            (int)Utility.NFactorial(7),
            (int)Utility.NFactorial(8),
            (int)Utility.NFactorial(9),
        };

        public override string Title => "Digit factorials";

        public override string Description => @"
145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

Find the sum of all numbers which are equal to the sum of the factorial of their digits.

Note: as 1! = 1 and 2! = 2 are not sums they are not included.
            ";

        public override string GetAnswer()
        {
            // Using an upper limit of 2540160 = 7 * 9!
            const int limit = 2540160;

            int sum = 0;

            for (int i = 10; i < limit; ++i)
            {
                if (i == GetSumOfFactorialDigits(i))
                {
                    sum += i;
                }
            }

            return sum.ToString();
        }

        private int GetSumOfFactorialDigits(int number)
        {
            return number.ToString().Sum(digit => _factorialCache[int.Parse(digit.ToString())]);
        }
    }
}