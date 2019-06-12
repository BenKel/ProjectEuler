using ProjectEuler.Models;

namespace ProjectEuler.Problems
{
    public class Problem57 : ProblemBase
    {
        public override string Title => "Square root convergents";

        public override string Description => @"
It is possible to show that the square root of two can be expressed as an infinite continued fraction.

√ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213...

By expanding this for the first four iterations, we get:

1 + 1/2 = 3/2 = 1.5
1 + 1/(2 + 1/2) = 7/5 = 1.4
1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666...
1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379...

The next three expansions are 99/70, 239/169, and 577/408, but the eighth expansion, 1393/985, is the first example where the number of digits in the numerator exceeds the number of digits in the denominator.

In the first one-thousand expansions, how many fractions contain a numerator with more digits than denominator?
            ";

        public override string GetAnswer()
        {
            int numeratorWithMoreDigitsCount = 0;

            for (int expansion = 1; expansion < 1001; expansion++)
            {
                var result = 1 + new BigFraction(1, GetExpansion(expansion));
                if (result.Numerator.ToString().Length > result.Denominator.ToString().Length)
                {
                    numeratorWithMoreDigitsCount++;
                }
            }

            return numeratorWithMoreDigitsCount.ToString();
        }

        private BigFraction GetExpansion(int count)
        {
            if (count == 1)
            {
                return new BigFraction(2, 1);
            }

            return 2 + new BigFraction(1, GetExpansion(count - 1));
        }
    }
}