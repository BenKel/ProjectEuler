namespace ProjectEuler
{
    internal class Problem49
    {
        // 1487, 4817, 8147 is a sequence where the terms increase by 3330, each term is prime and they are permutations of one another.
        // Returns the other 4-digit sequence that follows this rule.
        public string GetAnswer()
        {
            // This works for 1487, given in the example, so starting at 1489.
            for (int i = 1489; i < 10000; i+=2)
            {
                if (PrimeUtilities.IsPrime(i) && PrimeUtilities.IsPrime(i + 3330) && PrimeUtilities.IsPrime(i + 6660)
                    && StringUtilities.IsPermutation(i.ToString(), (i + 3330).ToString())
                    && StringUtilities.IsPermutation(i.ToString(), (i + 6660).ToString()))
                {
                    return i.ToString() + (i + 3330) + (i + 6660);
                }
            }

            return "not found";
        }
    }
}
