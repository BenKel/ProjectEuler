using ProjectEuler.Utilities;
using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Problems
{
    public class Problem49 : ProblemBase
    {
        private readonly IPrimeService _primeService;

        public Problem49(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public override string Title => "Prime permutations";

        public override string Description => @"
The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are permutations of one another.

There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.

What 12-digit number do you form by concatenating the three terms in this sequence?
            ";

        public override string GetAnswer()
        {
            // This works for 1487, given in the example, so starting at 1489.
            for (int i = 1489; i < 10000; i += 2)
            {
                if (_primeService.IsPrime(i) && _primeService.IsPrime(i + 3330) && _primeService.IsPrime(i + 6660)
                    && StringUtilities.IsPermutation(i.ToString(), (i + 3330).ToString())
                    && StringUtilities.IsPermutation(i.ToString(), (i + 6660).ToString()))
                {
                    return i.ToString() + (i + 3330) + (i + 6660);
                }
            }

            return "not found";
        }
    }
}