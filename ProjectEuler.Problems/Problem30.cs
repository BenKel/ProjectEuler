using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem30 : ProblemBase
    {
        public override string Title => "Digit fifth powers";

        public override string Description => @"
Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

    1634 = 1^4 + 6^4 + 3^4 + 4^4
    8208 = 8^4 + 2^4 + 0^4 + 8^4
    9474 = 9^4 + 4^4 + 7^4 + 4^4

As 1 = 1^4 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
            ";

        public override string GetAnswer()
        {
            int sum = 0;

            // Pretty abitrary limit here, 100,000 was too small.
            // I narrowed it down from 1,000,000.
            for (int i = 2; i < 200000; ++i)
            {
                if (i == Utility.SumOfPowerOfDigits(i, 5))
                {
                    sum += i;
                }
            }

            return sum.ToString();
        }
    }
}