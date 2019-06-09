using System.Numerics;

namespace ProjectEuler.Problems
{
    public class Problem48 : ProblemBase
    {
        public override string Title => "Self powers";

        public override string Description => @"
The series, 1^1 + 2^2 + 3^3 + ... + 10^10 = 10405071317.

Find the last ten digits of the series, 1^1 + 2^2 + 3^3 + ... + 1000^1000.
            ";

        public override string GetAnswer()
        {
            const int limit = 1000;
            BigInteger sum = BigInteger.Zero;

            for (int i = 1; i <= limit; ++i)
            {
                sum += BigInteger.Pow(i, i);
            }

            string lastTen = sum.ToString();
            lastTen = lastTen.Substring(lastTen.Length - 10);

            return lastTen;
        }
    }
}