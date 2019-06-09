using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem52 : ProblemBase
    {
        public override string Title => "Permuted multiples";

        public override string Description => @"
It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.

Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.
            ";

        public override string GetAnswer()
        {
            for (int x = 1; ; ++x)
            {
                if (StringUtilities.IsPermutation(x.ToString(), (2 * x).ToString())
                    && StringUtilities.IsPermutation((2 * x).ToString(), (3 * x).ToString())
                    && StringUtilities.IsPermutation((3 * x).ToString(), (4 * x).ToString())
                    && StringUtilities.IsPermutation((4 * x).ToString(), (5 * x).ToString())
                    && StringUtilities.IsPermutation((5 * x).ToString(), (6 * x).ToString()))
                {
                    return x.ToString();
                }
            }
        }
    }
}