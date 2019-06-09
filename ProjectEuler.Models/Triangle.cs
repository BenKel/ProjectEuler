using System;

namespace ProjectEuler.Models
{
    // A triangle with sides a <= b <= c
    public class Triangle
    {
        public Triangle(int sideA, int sideB, int sideC)
        {
            SideA = Math.Min(Math.Min(sideA, sideB), sideC);
            SideC = Math.Max(Math.Max(sideA, sideB), sideC);
            SideB = sideA + sideB + sideC - SideA - SideC;

            if (SideB > SideC || SideA > SideB)
            {
                throw new ArgumentException();
            }
        }

        public int SideA { get; }
        public int SideB { get; }
        public int SideC { get; }

        public int Perimeter => SideA + SideB + SideC;
        public int SideProduct => SideA * SideB * SideC;
        
        public static bool operator ==(Triangle a, Triangle b)
        {
            return a.SideA == b.SideA && a.SideB == b.SideB && a.SideC == b.SideC;
        }

        public static bool operator !=(Triangle a, Triangle b)
        {
            return a.SideA != b.SideA || a.SideB != b.SideB || a.SideC != b.SideC;
        }

        public override bool Equals(object o)
        {
            if (o == null || o.GetType() != GetType())
            {
                return false;
            }

            return this == o as Triangle;
        }

        public override int GetHashCode()
        {
            return SideB ^ SideA + SideC;
        }
    }
}
