using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Problems
{
    public class Problem37 : ProblemBase
    {
        private readonly IPrimeService _primeService;

        public Problem37(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public override string Title => "Truncatable primes";

        public override string Description => @"
The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.

Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
            ";

        public override string GetAnswer()
        {
            int count = 0;
            int sum = 0;

            // 2, 3, 5, 7 not considered truncatable, so start at 11.
            for (int i = 11; count < 11; i += 2)
            {
                if (!HasOddDigits(i) || !PrimeEndDigits(i) || !_primeService.IsPrime(i))
                {
                    continue;
                }

                if (IsTruncatable(i))
                {
                    ++count;
                    sum += i;
                }
            }

            return sum.ToString();
        }

        private bool PrimeEndDigits(int number)
        {
            string numberString = number.ToString();

            return IsPrimeChar(numberString[0]) && IsPrimeChar(numberString[numberString.Length - 1]);
        }

        private bool IsPrimeChar(char c)
        {
            return c == '2' || c == '3' || c == '5' || c == '7';
        }

        // Returns true if the number only contains odd digits or '2's
        private bool HasOddDigits(int number)
        {
            foreach (char c in number.ToString())
            {
                if (c == '1' || c == '2' || c == '3' || c == '5' || c == '7' || c == '9')
                {
                    continue;
                }
                return false;
            }

            return true;
        }

        private bool IsTruncatable(int number)
        {
            string numberString = number.ToString();
            // Check right hand side truncatable.
            for (int i = 1; i < numberString.Length; ++i)
            {
                int right = int.Parse(numberString.Substring(0, numberString.Length - i));
                int left = int.Parse(numberString.Substring(i, numberString.Length - i));

                if (!_primeService.IsPrime(right) || !_primeService.IsPrime(left))
                {
                    return false;
                }
            }

            return true;
        }
    }
}