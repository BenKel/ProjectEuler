using System;

namespace ProjectEuler
{
    internal class Problem45
    {
        public long GetAnswer()
        {
            // First hexagonal number after 40755, which is the first number to fit the rule.
            int n = 144;

            // every hexagonal number is triangular, so no need to test for that.
            long number = GetHexagonNumber(n);

           while (!IsPentagonalNumber(number))
            {
                ++n;
                number = GetHexagonNumber(n);
            }

            return number;
        }

        private static long GetHexagonNumber(int n)
        {
            return n * (2 * n - 1);
        }

        private static bool IsPentagonalNumber(long number)
        {
            double n = (Math.Sqrt(24 * number + 1) + 1) / 6;
            return Math.Abs(n % 1) < 0.000000001d;
        }
    }
}
