using ProjectEuler.Utilities.Arithmetic;

namespace ProjectEuler.Problems
{
    public class Problem12 : ProblemBase
    {
        private readonly IArithmeticService _arithmeticService;

        public Problem12(IArithmeticService arithmeticService)
        {
            _arithmeticService = arithmeticService;
        }

        public override string GetAnswer()
        {
            const int requiredNumberOfDivisors = 501;
            long triangleNumber = 0;

            for (int i = 1; ; ++i)
            {
                triangleNumber += i;

                int divisorCount = _arithmeticService.NumberOfDivisors(triangleNumber);

                if (divisorCount < requiredNumberOfDivisors)
                {
                    continue;
                }

                return triangleNumber.ToString();
            }
        }
    }
}