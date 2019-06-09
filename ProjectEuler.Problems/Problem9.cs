using System.Collections.Generic;
using ProjectEuler.Models;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem9 : ProblemBase
    {
        public override string GetAnswer()
        {
            const int tripletSum = 1000;

            List<Triangle> triangles = TriangleUtilities.GetRightTrianglesWithPerimeter(tripletSum);

            if (triangles.Count > 0)
            {
                return triangles[0].SideProduct.ToString();
            }

            return null;
        }
    }
}