using ProjectEuler.Utilities;
using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Problems
{
    public class Problem50 : ProblemBase
    {
        private readonly IPrimeService _primeService;

        public Problem50(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public override string Title => "Consecutive prime sum";

        public override string Description => @"
The prime 41, can be written as the sum of six consecutive primes:
41 = 2 + 3 + 5 + 7 + 11 + 13

This is the longest sum of consecutive primes that adds to a prime below one-hundred.

The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

Which prime, below one-million, can be written as the sum of the most consecutive primes?
            ";

        public override string GetAnswer()
        {
            // The sum of the primes up to 3943 = 1,001,604. No need to go higher.
            var primes = PrimeUtilities.GeneratePrimesUpToN(3943);

            // 547 primes under 3943.
            for (int length = 547; length > 0; --length)
            {
                for (int offset = 0; offset <= primes.Count - length; ++offset)
                {
                    int sum = 0;
                    for (int i = 0; i < length; ++i)
                    {
                        sum += primes[offset + i];
                    }

                    if (_primeService.IsPrime(sum))
                    {
                        return sum.ToString();
                    }
                }
            }

            return null;
        }
    }
}