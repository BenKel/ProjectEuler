using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem46 : ProblemBase
    {
        public override string Title => "Goldbach's other conjecture";

        public override string Description => @"
It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.

9 = 7 + 2×1^2
15 = 7 + 2×2^2
21 = 3 + 2×3^2
25 = 7 + 2×3^2
27 = 19 + 2×2^2
33 = 31 + 2×1^2

It turns out that the conjecture was false.

What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?
            ";

        public override string GetAnswer()
        {
            for (int i = 3; ; i += 2)
            {
                if (PrimeUtilities.IsPrime(i))
                {
                    continue;
                }

                if (FitsRule(i))
                {
                    return i.ToString();
                }
            }
        }

        private static bool FitsRule(int number)
        {
            // For each square less than the number, check to see if the difference is prime.
            for (int i = 1; 2 * i * i < number; ++i)
            {
                if (PrimeUtilities.IsPrime(number - (2 * i * i)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}