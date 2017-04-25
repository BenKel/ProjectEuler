using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    internal class Problem37
    {
        // Returns the sum of the 11 primes that are still rime when you truncate 1 digit at a time from both right and left.
        // i.e. 3797, 379, 37, 3.
        // 3797. 797, 79, 7.
        public int GetAnswer()
        {
            int count = 0;
            int sum = 0;

            // 2, 3, 5, 7 not considered truncatable, so start at 11.
            for (int i = 11; count < 11; i += 2)
            {
                if (!HasOddDigits(i) || !PrimeEndDigits(i) || !PrimeUtilities.IsPrime(i))
                {
                    continue;
                }

                if (IsTruncatable(i))
                {
                    ++count;
                    sum += i;
                }
            }

            return sum;
        }

        private static bool PrimeEndDigits(int number)
        {
            string numberString = number.ToString();

            return IsPrimeChar(numberString[0]) && IsPrimeChar(numberString[numberString.Length - 1]);
        }

        private static bool IsPrimeChar(char c)
        {
            return c == '2' || c == '3' || c == '5' || c == '7';
        }

        // Returns true if the number only contains odd digits or '2's
        private static bool HasOddDigits(int number)
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

        private static bool IsTruncatable(int number)
        {
            string numberString = number.ToString();
            // Check right hand side truncatable.
            for (int i = 1; i < numberString.Length; ++i)
            {
                int right = int.Parse(numberString.Substring(0, numberString.Length - i));
                int left = int.Parse(numberString.Substring(i, numberString.Length - i));

                if (!PrimeUtilities.IsPrime(right) || !PrimeUtilities.IsPrime(left))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
