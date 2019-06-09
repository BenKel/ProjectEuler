namespace ProjectEuler.Problems
{
    // Returns the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of an 
    // integer with (1,2,...,n) where n > 1.

    // Take the number 192 and multiply it by each of 1, 2, and 3:
    // 192 × 1 = 192
    // 192 × 2 = 384
    // 192 × 3 = 576
    // By concatenating each product we get the 1 to 9 pandigital, 192384576. We will call 192384576 the concatenated product of 192 and(1,2,3)
    // The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, which is the concatenated product of 9 and(1,2,3,4,5).
    public class Problem38 : ProblemBase
    {
        public override string GetAnswer()
        {
            // Start with a number. Count the digits and store it. Multiply it by two. Count the digits and store that.
            // Repeat until there are 9 digits. If the number of digits exceeds 9, move on to the next number.
            // Check that all of the digits 1-9 are present in the number.
            // If the number > the max concatenated product, store it. 

            // As the start of the concatenated product is always the number multiplied by 1,
            // and we are looking for the greatest product, I'll only look at numbers that begin with 9.

            // Given in the question, using 9 and (1,2,3,4,5).
            int maxConcatProduct = 918273645;

            // As n > 1, the largest number possible is 4 digits long, so I'll take 9999 as the limit.
            const int limit = 9999;

            for (int i = 90; i < limit; ++i)
            {
                if (i.ToString()[0] != '9')
                {
                    continue;
                }

                int digitCount = i.ToString().Length;
                string productString = i.ToString();

                for (int j = 2; digitCount < 9; ++j)
                {
                    int nextProduct = i*j;
                    digitCount += nextProduct.ToString().Length;
                    productString += nextProduct;
                }

                if (digitCount > 9)
                {
                    continue;
                }

                int product = int.Parse(productString);

                if (product > maxConcatProduct && IsPandigital(productString))
                {
                    maxConcatProduct = product;
                }
            }

            return maxConcatProduct.ToString();
        }

        private static bool IsPandigital(string number)
        {
            return number.Length == 9 && number.Contains("1") && 
                number.Contains("2") && number.Contains("3") && 
                number.Contains("4") && number.Contains("5") && 
                number.Contains("6") && number.Contains("7") && 
                number.Contains("8") && number.Contains("9");
        }
    }
}
