using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.Utilities.Arithmetic
{
    public class ArithmeticService : IArithmeticService
    {
        private readonly IPrimeService _primeService;

        public ArithmeticService(IPrimeService primeService)
        {
            _primeService = primeService;
        }

        public int NumberOfDivisors(long number)
        {
            int numberOfDivisors = 1;

            List<int> primeFactors = _primeService.GetPrimeFactors(number).ToList();

            // number of divisors d(n) = (a+1)(b+1)(c+1)...
            // where n = p^a * q^b * r^c ... (expressed as a product of prime factors)

            while (primeFactors.Count > 0)
            {
                int a = primeFactors[0];
                primeFactors.RemoveAt(0);
                int countOfA = 1;

                // Remove all other occurences of a.
                while (true)
                {
                    if (primeFactors.Contains(a))
                    {
                        ++countOfA;
                        primeFactors.Remove(a);
                        continue;
                    }
                    break;
                }

                numberOfDivisors *= countOfA + 1;
            }

            return numberOfDivisors;
        }
    }
}