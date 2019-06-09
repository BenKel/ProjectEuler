using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem10 : ProblemBase
    {
        public override string GetAnswer()
        {
            const int limit = 2000000;
            long sumOfPrimes = 5;

            for (int i = 5; i < limit; i += 2)
            {
                if (!PrimeUtilities.IsPrimeUncached(i))
                {
                    continue;
                }

                sumOfPrimes += i;
            }

            return sumOfPrimes.ToString();
        }
    }
}