using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem27 : ProblemBase
    {
        public override string Title => "Quadratic primes";

        public override string Description => @"
Euler discovered the remarkable quadratic formula:

n^2+n+41

It turns out that the formula will produce 40 primes for the consecutive integer values 0≤n≤39.
However, when n=40,40^2+40+41=40(40+1)+41 is divisible by 41, and certainly when n=41,41^2+41+41 is clearly divisible by 41.

The incredible formula n^2−79n+1601 was discovered, which produces 80 primes for the consecutive values 0≤n≤79.
The product of the coefficients, −79 and 1601, is −126479.

Considering quadratics of the form:

    n^2+an+b, where |a|<1000 and |b|≤1000

where |n| is the modulus/absolute value of n
e.g. |11|=11 and |−4|=4

Find the product of the coefficients, a and b,
for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n=0.
";

        public override string GetAnswer()
        {
            // Let's assume that the number of consecutive primes < b.
            int bestA = 1;
            int bestB = 41;
            int consecutivePrimeCount = 40;

            for (int b = 1000; b >= -1000; --b)
            {
                if (!PrimeUtilities.IsPrime(b))
                {
                    continue;
                }

                for (int a = -1000; a < 1000; ++a)
                {
                    if (!PrimeUtilities.IsPrime(1 + a + b))
                    {
                        continue;
                    }

                    // Check length of consecutive prime list.
                    int n = 2;
                    while (PrimeUtilities.IsPrime((n * n) + (a * n) + b))
                    {
                        ++n;
                    }

                    if (n > consecutivePrimeCount)
                    {
                        consecutivePrimeCount = n;

                        bestA = a;
                        bestB = b;
                    }
                }
            }

            return (bestA * bestB).ToString();
        }
    }
}