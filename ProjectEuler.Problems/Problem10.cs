using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Problems
{
    public class Problem10 : ProblemBase
    {
        private readonly IPrimeService _primeService;

        public Problem10(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public override string GetAnswer()
        {
            const int limit = 2000000;
            long sumOfPrimes = 5;

            for (int i = 5; i < limit; i += 2)
            {
                if (!_primeService.IsPrimeUncached(i))
                {
                    continue;
                }

                sumOfPrimes += i;
            }

            return sumOfPrimes.ToString();
        }
    }
}