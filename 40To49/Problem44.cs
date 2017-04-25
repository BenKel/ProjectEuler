using System;

namespace ProjectEuler
{
    internal class Problem44
    {
        // Let Pj and Pk be a pair of pentagonal numbers. Pj + Pk is also a pentagonal number, as is D = Pk - Pj.
        // Returns the smallest D value.
        public long GetAnswer()
        {
            long bestD = int.MaxValue;

            // The limit is pretty abitrary, couldn't see an easy way to find one.
            const int limit = 5000;

            for (int kn = 1; kn < limit; ++kn)
            {
                long k = GetPentagonalNumber(kn);

                if (GetPentagonalNumber(kn + 1) - k >= bestD)
                {
                    break;
                }

                for (int jn = kn + 1; jn < limit; ++jn)
                {
                    long j = GetPentagonalNumber(jn);

                    if (j - k >= bestD)
                    {
                        break;
                    }

                    if (!IsPentagonalNumber(j + k) || !IsPentagonalNumber(j - k))
                    {
                        continue;
                    }
                    
                    bestD = j - k;
                    break;
                }
            }

            return bestD;
        }

        private static long GetPentagonalNumber(int n)
        {
            long pentagon = n*(3L*n - 1)/2;
            return pentagon;
        }

        private static bool IsPentagonalNumber(long number)
        {
            double n = (Math.Sqrt(24 * number + 1) + 1) / 6;
            return Math.Abs(n % 1) < 0.000000001d;
        }

    }
}
