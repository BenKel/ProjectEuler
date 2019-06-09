using System.Numerics;

namespace ProjectEuler.Problems
{
    public class Problem56 : ProblemBase
    {
        public override string Title => "Powerful digit sum";

        public override string Description => @"
A googol (10^100) is a massive number: one followed by one-hundred zeros; 100^100 is almost unimaginably large: one followed by two-hundred zeros. Despite their size, the sum of the digits in each number is only 1.

Considering natural numbers of the form, a^b, where a, b < 100, what is the maximum digital sum?
            ";

        public override string GetAnswer()
        {
            int maxSum = 0;

            for (int b = 99; b > 0; b--)
            {
                for (int a = 99; a > 0; a--)
                {
                    var currentNumber = BigInteger.Pow(new BigInteger(a), b);

                    var sum = SumDigits(currentNumber);

                    if (sum > maxSum)
                    {
                        maxSum = sum;
                    }
                }
            }
            return maxSum.ToString();
        }

        private static int SumDigits(BigInteger number)
        {
            int sum = 0;

            foreach (var digit in number.ToString())
            {
                sum += int.Parse(digit.ToString());
            }

            return sum;
        }
    }
}