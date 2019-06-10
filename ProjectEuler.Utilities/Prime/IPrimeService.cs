using System.Numerics;

namespace ProjectEuler.Utilities.Prime
{
    public interface IPrimeService
    {
        bool IsPrime(long number);

        bool IsPrimeUncached(long number);

        bool IsPrime(BigInteger number);

        bool IsPrimeUncached(BigInteger number);
    }
}