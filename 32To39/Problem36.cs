using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    internal class Problem36
    {
        // Returns the sum of all numbers < 1,000,000 which are palindromic in base 2 and 10.
        public int GetAnswer()
        {
            const int limit = 1000000;
            int sum = 0;

            for (int i = 1; i < limit; ++i)
            {
                if (StringUtilities.IsPalindrome(i.ToString())
                    && StringUtilities.IsPalindrome(Convert.ToString(i, 2)))
                {
                    sum += i;
                }
            }

            return sum;
        }
    }
}
