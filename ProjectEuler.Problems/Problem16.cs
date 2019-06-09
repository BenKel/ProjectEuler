using System.Linq;
using System.Numerics;

namespace ProjectEuler.Problems
{
    public class Problem16 : ProblemBase
    {
        public override string GetAnswer()
        {
            const int powerOfTwo = 1000;
            var number = new BigInteger(1);

            for (int i = 0; i < powerOfTwo; ++i)
            {
                number *= 2;
            }

            char[] numberChars = number.ToString().ToCharArray();

            int sum = numberChars.Sum(digit => (int)char.GetNumericValue(digit));

            return sum.ToString();
        }
    }
}