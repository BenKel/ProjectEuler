namespace ProjectEuler
{
    internal class Problem52
    {
        // Returns the smallest positive integer, x, such that 2x, 3x, 4x, 5x and 6x contain the same digits.
        public int GetAnswer()
        {
            for (int x = 1;; ++x)
            {
                if (StringUtilities.IsPermutation(x.ToString(), (2*x).ToString())
                    && StringUtilities.IsPermutation((2*x).ToString(), (3*x).ToString())
                    && StringUtilities.IsPermutation((3*x).ToString(), (4*x).ToString())
                    && StringUtilities.IsPermutation((4*x).ToString(), (5*x).ToString())
                    && StringUtilities.IsPermutation((5*x).ToString(), (6*x).ToString()))
                {
                    return x;
                }
            }
        }
    }
}
