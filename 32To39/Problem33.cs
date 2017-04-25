using System;
using System.Collections.Generic;
using ProjectEuler.DataTypes;

namespace ProjectEuler
{
    internal class Problem33
    {
        // TODO: flesh out fraction class with comparison operators and use that instead of this 
        // find the four fractions (a/b < 1. a,b % 10 != 0) where a digit can be cancelled from the top and the botrtom to yield an equal fraction.
        // eg: 49/98 = 4/8. the 9 cancels. (mathematically incorrect method, but the correct answer).
        // This method returns the the denominator of the product of these four fractions, given in its lowest terms.
        public int GetAnswer()
        {
            // To make fractions of the form (10a+b)/(10c+d). 10a+b < 10c+d

            // [0] is first numerator, [1] is first denominator
            var chosenFractions = new List<Fraction>();

            for (int c = 1, d = 1; c < 10; ++d)
            {
                if (d == 10)
                {
                    // Skip d = 0 because these fractions are considered trivial and therefore should not be included.
                    d = 1;
                    ++c;
                }

                for (int a = 1, b = 1; a < c || b < d; ++b)
                {
                    if (b == 10)
                    {
                        // Skip b = 0 because these fractions are considered trivial and therefore should not be included.
                        b = 1;
                        ++a;

                        if (a >= c && b >= d)
                        {
                            break;
                        }
                    }

                    if (ShareACommonDigit(a, b, c, d))
                    {
                        // Try cancelling this digit to see if the fraction is equal.
                        var originalFraction = new Fraction(10*a + b, 10*c + d);

                        if (b == 0 || d == 0 || (a == b && c == d))
                        {
                            continue;
                        }

                        Fraction cancelledFraction = GetCancelledFraction(a, b, c, d);

                        // If the fractions are equal, add the original to the array storing them.
                        if (originalFraction == cancelledFraction)
                        {
                            //if (cancelledFraction.Numerator != originalFraction.Numerator%10 &&
                            //    cancelledFraction.Denominator != originalFraction.Denominator%10)
                            chosenFractions.Add(new Fraction(10*a + b, 10*c + d));
                        }
                    }
                }
            }

            return ListToAnswer(chosenFractions);
        }

        private static bool ShareACommonDigit(int a, int b, int c, int d)
        {
            return a == c || a == d || b == c || d == c;
        }

        private static Fraction GetCancelledFraction(int a, int b, int c, int d)
        {
            Fraction fraction;

            if (a == c)
            {
                fraction = new Fraction(b, d);
            }
            else if (a == d)
            {
                fraction = new Fraction(b, c);
            }
            else if (b == c)
            {
                fraction = new Fraction(a, d);
            }
            else
            {
                fraction = new Fraction(a, c);
            }

            return fraction;
        }

        private static int ListToAnswer(List<Fraction> fractions)
        {
            Fraction product = Fraction.One;

            foreach (Fraction fraction in fractions)
            {
                product *= fraction;
            }

            product.Simplify();

            return product.Denominator;
        }
    }
}
