using System.Numerics;

namespace ProjectEuler
{
    internal class Problem53
    {
        private const int Limit = 1000000;

        public int GetAnswer()
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

            return count;
        }

        private static bool NChooseR(int n, int r)
        {
            BigInteger result = Utility.NFactorial(n)/(Utility.NFactorial(r)*Utility.NFactorial(n - r));

            return result > Limit;
        }
    }
}
