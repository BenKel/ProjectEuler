using System;
using System.Collections.Generic;
using ProjectEuler.DataTypes;

namespace ProjectEuler
{
    internal static class TriangleUtilities
    {
        public static List<Triangle> GetRightTrianglesWithPerimeter(int perimeter)
        {
            var triangleSet = new HashSet<Triangle>();
            if (perimeter%2 != 0)
            {
                return new List<Triangle>();
            }

            // use euclid's formula: a = m^2 - n^2, b = 2mn, c = m^2 + n^2
            // so m^2 + mn = perimeter/2.
            // and sqrt(limit/2) > m > n > 0

            for (int n = 1; n*n < perimeter/2; ++n)
            {
                for (int m = n+1; m*m <= perimeter/2; ++m)
                {
                    int halfP = m*(m + n);
                    if (halfP > perimeter/2) continue;

                    // Check if there is a k such that ka + kb + kc = perimeter.
                    if (halfP < perimeter/2)
                    {
                        for (int k = 2; k*halfP <= perimeter/2; ++k)
                        {
                            if (k*halfP == perimeter/2)
                            {
                                // Correct m and n found, add to list.
                                int a = k * ((m*m) - (n*n));
                                int b = 2*k*m*n;
                                int c = k*((m*m) + (n*n));

                                var triangle = new Triangle(a, b, c);
                                triangleSet.Add(triangle);
                            }
                        }
                    }
                    else
                    {
                        // Correct m and n found, add to list.
                        int a = (m*m) - (n*n);
                        int b = 2*m*n;
                        int c = (m*m) + (n*n);

                        var triangle = new Triangle(a, b, c);
                        triangleSet.Add(triangle);
                    }
                }
            }

            return new List<Triangle>(triangleSet);
        }
    }
}
