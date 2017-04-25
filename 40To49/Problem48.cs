using System.Numerics;

namespace ProjectEuler
{
    internal class Problem48
    {
        // Returns the last 10 digits of the series 1^1 + 2^2 + 3^3 + ... + 1000^1000
        public string GetAnswer()
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
