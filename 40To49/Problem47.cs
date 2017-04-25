using System.Collections.Generic;

namespace ProjectEuler
{
    internal class Problem47
    {
        // Returns the first number from the sequence of four consecutive integers to have four distinct prime factors each.
        public int GetAnswer()
        {
            var numbers = new Queue<List<int>>();
            numbers.Enqueue(PrimeUtilities.PrimeFactors(1));
            numbers.Enqueue(PrimeUtilities.PrimeFactors(2));
            numbers.Enqueue(PrimeUtilities.PrimeFactors(3));

            for (int i = 4; ; ++i)
            {
                List<int> newFactors = PrimeUtilities.PrimeFactors(i);

                CondenseDuplicates(newFactors);

                numbers.Enqueue(newFactors);

                bool found = true;

                foreach (List<int> factors in numbers)
                {
                    if (factors.Count != 4)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }

                numbers.Dequeue();
            }

            List<int> primeFactors = numbers.Dequeue();
            int product = 1;

            foreach (int factor in primeFactors)
            {
                product *= factor;
            }
            return product;
        }

        private static void CondenseDuplicates(List<int> factors)
        {
            // Remove any duplicate prime factors, so 644: 2, 2, 7, 23 would become 644: 4, 7, 23.
            for (int j = factors.Count - 1; j > 0; --j)
            {
                int value = factors[j];
                for (int k = j - 1; k >= 0; --k)
                {
                    if (factors[k] == value)
                    {
                        factors[j] *= factors[k];
                        factors.RemoveAt(k);
                        --j;
                    }
                }
            }
        }

        private static bool ContainsDuplicates(Queue<List<int>> queue)
        {
            var array = queue.ToArray();
            var list = new List<int>();
            foreach (List<int> primeList in array)
            {
                list.AddRange(primeList);
            }

            return Utility.AnyDuplicates(list);
        }
    }
}
