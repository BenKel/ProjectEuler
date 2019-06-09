using System;

namespace ProjectEuler.Models
{
    internal class Fraction
    {
        public Fraction()
        {
            Initialise(0, 1);
        }

        public Fraction(int numerator)
        {
            Initialise(numerator, 1);
        }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be 0.");
            }

            Initialise(numerator, denominator);
        }

        public static readonly Fraction One = new Fraction(1, 1);
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public static Fraction operator + (Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int denominator = a.Denominator * b.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator - (Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            int denominator = a.Denominator * b.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator * (Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Numerator;
            int denominator = a.Denominator * b.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator / (Fraction a, Fraction b)
        {
            int numerator = a.Numerator * b.Denominator;
            int denominator = a.Denominator * b.Numerator;

            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(numerator, denominator);
        }

        public static bool operator == (Fraction a, Fraction b)
        {
            return a.Numerator*b.Denominator == a.Denominator*b.Numerator;
        }

        public static bool operator != (Fraction a, Fraction b)
        {
            return a.Numerator*b.Denominator != a.Denominator*b.Numerator;
        }
        
        public static bool operator > (Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
        }

        public static bool operator < (Fraction a, Fraction b)
        {
            return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
        }

        public static bool operator >= (Fraction a, Fraction b)
        {
            return !(a < b);
        }

        public static bool operator <= (Fraction a, Fraction b)
        {
            return !(a > b);
        }

        public override bool Equals(object o)
        {
            if (o == null || o.GetType() != GetType())
            {
                return false;
            }

            return this == o as Fraction;
        }

        public override int GetHashCode()
        {
            return Numerator ^ Denominator;
        }

        private void Initialise(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        
        public void Simplify()
        {
            int limit = Math.Min(Numerator, Denominator);

            for (int i = 2; i <= limit; ++i)
            {
                if (Numerator%i != 0 || Denominator%i != 0)
                {
                    continue;
                }

                Numerator /= i;
                Denominator /= i;
                limit = Math.Min(Numerator, Denominator);
                i = 1;
            }
        }
    }
}
