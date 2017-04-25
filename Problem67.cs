using System;

namespace ProjectEuler
{
    internal class Problem67
    {
        // Maximum path down a triangle of numbers 2
        public int GetAnswer()
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

            return sum;
        }
    }
}
