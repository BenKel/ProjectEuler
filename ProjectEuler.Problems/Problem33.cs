using System.Collections.Generic;
using ProjectEuler.Models;

namespace ProjectEuler.Problems
{
    public class Problem33 : ProblemBase
    {
        public override string Title => "Digit cancelling fractions";

        public override string Description => @"
The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

There are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.

If the product of these four fractions is given in its lowest common terms, find the value of the denominator.
            ";

        public override string GetAnswer()
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
                        var originalFraction = new Fraction(10 * a + b, 10 * c + d);

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
                            chosenFractions.Add(new Fraction(10 * a + b, 10 * c + d));
                        }
                    }
                }
            }

            return ListToAnswer(chosenFractions).ToString();
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