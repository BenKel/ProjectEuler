﻿using System.Numerics;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem53 : ProblemBase
    {
        private const int Limit = 1000000;

        public override string Title => "Number spiral diagonals";

        public override string Description => @"
There are exactly ten ways of selecting three from five, 12345:

123, 124, 125, 134, 135, 145, 234, 235, 245, and 345

In combinatorics, we use the notation, 5C3=10.

In general, nCr=n!r!(n−r)!, where r≤n, n!=n×(n−1)×...×3×2×1, and 0!=1.

It is not until n=23, that a value exceeds one-million: 23C10=1144066.

How many, not necessarily distinct, values of nCr
for 1≤n≤100, are greater than one-million?
            ";

        public override string GetAnswer()
        {
            int count = 0;

            for (int n = 23; n <= 100; ++n)
            {
                for (int r = 1; r <= n; ++r)
                {
                    bool isGreater = NChooseR(n, r);

                    if (isGreater)
                    {
                        ++count;
                    }
                }
            }

            return count.ToString();
        }

        private static bool NChooseR(int n, int r)
        {
            BigInteger result = Utility.NFactorial(n) / (Utility.NFactorial(r) * Utility.NFactorial(n - r));

            return result > Limit;
        }
    }
}