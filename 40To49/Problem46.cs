namespace ProjectEuler
{
    internal class Problem46
    {
        // Returns the smallest odd composite number that cannot be written as the sum of a prime and twice a square.
        public int GetAnswer()
        {
            for (int i = 3; ; i += 2)
            {
                if (PrimeUtilities.IsPrime(i))
                {
                    continue;
                }

                if (FitsRule(i))
                {
                    return i;
                }
            }
        }

        private static bool FitsRule(int number)
        {
            // For each square less than the number, check to see if the difference is prime.
            for (int i = 1; 2*i*i < number; ++i)
            {
                if (PrimeUtilities.IsPrime(number - (2*i*i)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
