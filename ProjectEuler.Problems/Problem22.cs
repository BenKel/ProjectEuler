using System.Collections.Generic;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem22 : ProblemBase
    {
        public override string Title => "Names scores";

        public override string Description => @"
Using a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.

For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938 × 53 = 49714.

What is the total of all the name scores in the file?
            ";

        public override string GetAnswer()
        {
            const string filePath = "Data/Problem22Data.txt";

            List<string> list = Utility.ParseFileToStringList(filePath);

            list.Sort();

            long sum = 0;

            for (int i = 0; i < list.Count; ++i)
            {
                sum += StringUtilities.AlphabeticalScore(list[i]) * (i + 1);
            }

            return sum.ToString();
        }
    }
}