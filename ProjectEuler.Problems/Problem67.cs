using System;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem67 : ProblemBase
    {
        public override string Title => "Maximum path sum II";

        public override string Description => @"
By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.

3
7 4
2 4 6
8 5 9 3

That is, 3 + 7 + 4 + 9 = 23.

Find the maximum total from top to bottom in triangle.txt (right click and 'Save Link/Target As...'), a 15K text file containing a triangle with one-hundred rows.
            ";

        public override string GetAnswer()
        {
            int sum = 0;

            // Start on the bottom row. Look for the greatest number.
            // represent each number as the sum of itself and the greatest number above it
            int[,] data = Utility.ParseFileToIntArray("Data/Problem67Data.txt", 100, 100);

            for (int i = 1; i < data.GetLength(0); ++i)
            {
                data[i, 0] += data[i - 1, 0];

                for (int j = 1; j < i + 1; ++j)
                {
                    data[i, j] += Math.Max(data[i - 1, j], data[i - 1, j - 1]);
                }
            }

            for (int i = 0; i < data.GetLength(1); ++i)
            {
                sum = Math.Max(data[data.GetLength(0) - 1, i], sum);
            }

            return sum.ToString();
        }
    }
}