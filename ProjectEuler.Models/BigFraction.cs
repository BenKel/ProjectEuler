using System;
using System.Numerics;

namespace ProjectEuler.Models
{
    public class BigFraction
    {
        public BigFraction(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        public BigFraction(int numerator, BigFraction denominator)
        {
            Numerator = numerator * denominator.Denominator;
            Denominator = denominator.Numerator;
        }

        public BigFraction(BigFraction numerator, BigFraction denominator)
        {
            Numerator = numerator.Numerator * denominator.Denominator;
            Denominator = denominator.Numerator * numerator.Denominator;
        }

        public BigInteger Numerator { get; }
        public BigInteger Denominator { get; }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public BigFraction Simplify()
        {
            var gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
            return new BigFraction(Numerator / gcd, Denominator / gcd);
        }

        public static BigFraction operator +(BigFraction a, BigFraction b)
        {
            return new BigFraction((a.Numerator * b.Denominator) + (b.Numerator * a.Denominator),
                a.Denominator * b.Denominator);
        }

        public static BigFraction operator +(BigFraction a, int b)
        {
            return a + new BigFraction(b, 1);
        }

        public static BigFraction operator +(int a, BigFraction b)
        {
            return b + a;
        }

        public static BigFraction operator *(BigFraction a, BigFraction b)
        {
            return new BigFraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static BigFraction operator -(BigFraction a, BigFraction b)
        {
            return a + new BigFraction(-b.Numerator, b.Denominator);
        }
    }
}