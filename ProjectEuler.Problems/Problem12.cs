using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem12 : ProblemBase
    {
        public override string GetAnswer()
        {
            const int requiredNumberOfDivisors = 501;
            long triangleNumber = 0;

            for (int i = 1; ; ++i)
            {
                triangleNumber += i;

                int divisorCount = Utility.NumberOfDivisors(triangleNumber);

                if (divisorCount < requiredNumberOfDivisors)
                {
                    continue;
                }

                return triangleNumber.ToString();
            }
        }
    }
}