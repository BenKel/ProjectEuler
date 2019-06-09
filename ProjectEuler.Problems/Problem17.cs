using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem17 : ProblemBase
    {
        public override string GetAnswer()
        {
            int sum = 0;
            int sumUpTo100 = 0;

            // All numbers up to 100;
            for (int i = 1; i < 100; ++i)
            {
                sumUpTo100 += Utility.CountLettersInNumber(i);
            }

            sum += sumUpTo100;

            for (int i = 1; i < 10; ++i)
            {
                var hundredLetterCount = Utility.CountLettersInNumber(i * 100);
                sum += hundredLetterCount;

                // + 'and'
                hundredLetterCount += 3;

                sum += 99 * hundredLetterCount;
                sum += sumUpTo100;
            }

            // 'one thousand'
            sum += 11;

            return sum.ToString();
        }
    }
}