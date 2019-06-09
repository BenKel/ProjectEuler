using System.Collections.Generic;
using ProjectEuler.Models;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem39 : ProblemBase
    {
        public override string Title => "Integer right triangles";

        public override string Description => @"
If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

{20,48,52}, {24,45,51}, {30,40,50}

For which value of p ≤ 1000, is the number of solutions maximised?
            ";

        public override string GetAnswer()
        {
            // Iterate through values of p.
            // Find all values of a, b, c where a + b + c = p AND a^2 + b^2 = c^2 (AND a <= b < c)
            const int limit = 1000;
            int bestP = 0;
            int bestPCount = 0;

            // 3 + 4 + 5 = 12, start there.
            for (int p = limit; p >= 12; --p)
            {
                List<Triangle> triangles = TriangleUtilities.GetRightTrianglesWithPerimeter(p);
                if (triangles.Count > bestPCount)
                {
                    bestP = p;
                    bestPCount = triangles.Count;
                }
            }

            return bestP.ToString();
        }
    }
}