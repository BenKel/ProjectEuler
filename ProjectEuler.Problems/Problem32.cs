﻿using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem32 : ProblemBase
    {
        public override string Title => "Pandigital products";

        public override string Description => @"
We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.

The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.

Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.
            ";
        
        public override string GetAnswer()
        {
            // Most will be of the form ## * ### = ####
            // a * b = c
            // 12 <= a <= 98, 123 <= b < 834   (12 * 834 > 9999)

            var products = new HashSet<int>();

            for (int a = 12; a <= 98; ++a)
            {
                for (int b = 123; b < 834; ++b)
                {
                    int product = a * b;
                    string productString = product.ToString();
                    string aString = a.ToString();
                    string bString = b.ToString();

                    // Continue if the product's length is greater than 4.
                    if (productString.Length != 4)
                    {
                        continue;
                    }

                    // Continue if there are any zeroes.
                    if (aString.Contains('0') || bString.Contains('0') || productString.Contains('0'))
                    {
                        continue;
                    }

                    // Continue if any number has duplicate digits.
                    if (StringUtilities.HasDuplicateChars(aString) || StringUtilities.HasDuplicateChars(bString) || StringUtilities.HasDuplicateChars(productString))
                    {
                        continue;
                    }

                    // Continue if a and b share any digits, or a and c, or b and c.
                    if (aString.Any(c => bString.Contains(c)) || aString.Any(c => productString.Contains(c)) || bString.Any(c => productString.Contains(c)))
                    {
                        continue;
                    }

                    products.Add(product);
                }
            }

            // There are also some of the form # * #### = ####
            for (int a = 1; a < 9; ++a)
            {
                for (int b = 1234; b < 9876; ++b)
                {
                    int product = a * b;
                    string productString = product.ToString();
                    string aString = a.ToString();
                    string bString = b.ToString();

                    // Continue if the product's length is greater than 4.
                    if (productString.Length != 4)
                    {
                        continue;
                    }

                    // Continue if there are any zeroes.
                    if (aString.Contains('0') || bString.Contains('0') || productString.Contains('0'))
                    {
                        continue;
                    }

                    // Continue if any number has duplicate digits.
                    if (StringUtilities.HasDuplicateChars(aString) || StringUtilities.HasDuplicateChars(bString) || StringUtilities.HasDuplicateChars(productString))
                    {
                        continue;
                    }

                    // Continue if a and b share any digits, or a and c, or b and c.
                    if (aString.Any(c => bString.Contains(c)) || aString.Any(c => productString.Contains(c)) || bString.Any(c => productString.Contains(c)))
                    {
                        continue;
                    }

                    products.Add(product);
                }
            }

            return products.Sum().ToString();
        }
    }
}