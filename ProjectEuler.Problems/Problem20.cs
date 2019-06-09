using System.Linq;
using System.Numerics;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem20 : ProblemBase
    {
        public override string GetAnswer()
        {
            const int nthFactorial = 100;
            BigInteger factorial = Utility.NFactorial(nthFactorial);

            char[] digits = factorial.ToString().ToCharArray();

            return digits.Sum(digit => (int)char.GetNumericValue(digit)).ToString();
        }
    }
}