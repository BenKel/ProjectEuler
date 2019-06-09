using System.Linq;
using System.Numerics;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem25 : ProblemBase
    {
        public override string Title => "1000-digit Fibonacci number";
        public override string Description => @"
The Fibonacci sequence is defined by the recurrence relation:

    Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.

Hence the first 12 terms will be:

    F1 = 1
    F2 = 1
    F3 = 2
    F4 = 3
    F5 = 5
    F6 = 8
    F7 = 13
    F8 = 21
    F9 = 34
    F10 = 55
    F11 = 89
    F12 = 144

The 12th term, F12, is the first term to contain three digits.

What is the index of the first term in the Fibonacci sequence to contain 1000 digits?
";

        public override string GetAnswer()
        {
            var f1 = new BigInteger(1);
            var f2 = new BigInteger(1);
            int index = 2;

            while (f1.ToString().Length < 1000)
            {
                ++index;
                BigInteger temp = f1;

                f1 = BigInteger.Add(f1, f2);
                f2 = temp;
            }

            return index.ToString();
        }
    }
}