using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem14 : ProblemBase
    {
        public override string GetAnswer()
        {
            int longestSequence = 0;
            int titleHolder = 0;

            // Going backwards through the loop speeds things up.
            for (int i = 1000000; i > 1; --i)
            {
                int seqLength = Utility.CollatzSequenceLength(i);

                if (seqLength > longestSequence)
                {
                    longestSequence = seqLength;
                    titleHolder = i;
                }
            }

            return titleHolder.ToString();
        }
    }
}