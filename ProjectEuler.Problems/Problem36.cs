using System;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem36 : ProblemBase
    {
        public override string Title => "Double-base palindromes";

        public override string Description => @"
The decimal number, 585 = 1001001001 (binary), is palindromic in both bases.

Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

(Please note that the palindromic number, in either base, may not include leading zeros.)
            ";
        
        public override string GetAnswer()
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

            return sum.ToString();
        }
    }
}