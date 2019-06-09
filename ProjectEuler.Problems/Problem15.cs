namespace ProjectEuler.Problems
{
    public class Problem15 : ProblemBase
    {
        public override string GetAnswer()
        {
            const int gridWidth = 20;

            var grid = new long[gridWidth + 1, gridWidth + 1];

            for (int i = 0; i < gridWidth; ++i)
            {
                grid[i, gridWidth] = 1;
                grid[gridWidth, i] = 1;
            }

            for (int x = gridWidth - 1; x >= 0; --x)
            {
                for (int y = gridWidth - 1; y >= 0; --y)
                {
                    grid[x, y] = grid[x + 1, y] + grid[x, y + 1];
                }
            }

            return grid[0, 0].ToString();
        }
    }
}