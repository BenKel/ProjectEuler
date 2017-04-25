using System.Linq;
using System.Numerics;
using System.Collections.Generic;

namespace ProjectEuler
{
    // Most of the problems are short, so they're all here together.
    internal class Problems21To30
    {
        // Sum of amicable numbers under a limit.
        // Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
        // If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
        private static int Problem21(int limit)
        {
            // Calculate the sum of amicables as we go. 
            // If d(n) > n, ignore it. If d(n) < n, count it. This will avoid duplicates.
            int sumOfAmicables = 0;

            var factorSums = new Dictionary<int, int>();

            for (int i = 2; i < limit; ++i)
            {
                int factorSum = Utility.SumFactors(i);

                if (factorSum >= limit || factorSum == i)
                {
                    continue;
                }

                if (factorSum > i)
                {
                    factorSums.Add(i, factorSum);
                }
                else if (factorSums.ContainsKey(factorSum) && factorSums[factorSum] == i)
                {
                    sumOfAmicables += i;
                    sumOfAmicables += factorSum;
                }
            }

            return sumOfAmicables;
        }

        // Total of names scores in a file.
        // Alphabetical score * list position in alphabetical order.
        private static long Problem22()
        {
            const string filePath = "Data/Problem22Data.txt";

            List<string> list = Utility.ParseFileToStringList(filePath);

            list.Sort();

            long sum = 0;

            for (int i = 0; i < list.Count; ++i)
            {
                sum += StringUtilities.AlphabeticalScore(list[i]) * (i + 1);
            }

            return sum;
        }

        // Sum of all numbers that cannot be expressed as the sum of two abundant numbers.
        // 500ms
        // Now 400ms
        // Now 220ms
        // Now 130ms
        private static int Problem23()
        {
            // Every number above 20161 can be written as the sum of two abundants.
            const int limit = 20161;

            List<int> abundantNumbers = Utility.GetAbundantNumbers(limit);

            var naturalNumbers = new int[limit];

            for (int i = 0; i < naturalNumbers.Length; ++i)
            {
                naturalNumbers[i] = i + 1;
            }

            for (int i = 0; i < abundantNumbers.Count; ++i)
            {
                for (int j = i; abundantNumbers[i] + abundantNumbers[j] <= limit; ++j)
                {
                    naturalNumbers[abundantNumbers[i] + abundantNumbers[j] - 1] = 0;
                }
            }

            int sum = naturalNumbers.Sum();

            return sum;
        }

        // Millionth lexicographical permutation of 0123456789
        private static string Problem24()
        {
            // 3,628,800 ways of arranging in total.

            // 1,000,000 = 2*9! + 6*8! + 6*7! + 2*6! + 5*5! + 4! + 2*3! + 2*2!
            // 2*9! means that the third number in the list will be in the first position.
            // 6*8! means that the 7th number from the list (with '2' removed) will be in the second position.
            // Applying this rule to the whole list, we get
            return "2783915460";
        }

        // 1000 digit fibonacci number.
        // What is the index of the first term in the fibonacci sequence to contain 1000 digits?
        // TODO reduce from ~200ms
        private static int Problem25()
        {
            var f1 = new BigInteger(1);
            var f2 = new BigInteger(1);
            int index = 2;

            while (f1.ToString().Length < 1000)
            {
                ++index;
                BigInteger temp = f1;

                f1 = BigInteger.Add(f1, f2);
                f2 = temp;
            }

            return index;
        }

        // Unit fraction 1/d, d < 1000, with the longest recurring cycle when represented as a decimal.
        // After spending a lot of time trying to calculate the fraction and measure the length of the decimal period,
        // I looked into ways of measuring the length of the decimal without having to calculate it.
        private static int Problem26()
        {
            // "Any nonregular fraction m/n is periodic and has decimal period lambda(n) independent of m, which is at most n-1 digits long."
            // So I can work down from 1000, and when the max decimal period >= the current denominator I can stop.
            const int limit = 1000;
            int chosenDenominator = 2;
            int maxSequenceLength = 0;

            for (int d = limit - 1; maxSequenceLength <= d && d > 2; --d)
            {
                // Calculate the remainder when 1 is divided by d, storing it each time and multiplying the remainder by 10.
                // This is just like bus stop division where the remainder is carried over to the next decimal place.
                // When a remainder is reached for the second time I can stop calculating, because the sequence of remainders will repeat.

                int sequenceLength;
                int remainder = 1;
                int index = 0;
                var remainderArray = new int[d];

                while (true)
                {
                    remainder *= 10;
                    remainder = remainder % d;

                    if (remainderArray[remainder] != 0)
                    {
                        sequenceLength = index - 1 - remainderArray[remainder];
                        break;
                    }

                    remainderArray[remainder] = index + 1;

                    ++index;
                }

                if (maxSequenceLength < sequenceLength)
                {
                    maxSequenceLength = sequenceLength;
                    chosenDenominator = d;
                }
            }

            return chosenDenominator;
        }

        // n^2 + an + b, |a| < 1000, |b| <= 1000
        // That produces the most consecutive primes, starting at n = 0.
        // TODO: clean this up
        private static int Problem27()
        {
            // Let's assume that the number of consecutive primes < b.
            int bestA = 1;
            int bestB = 41;
            int consecutivePrimeCount = 40;

            for (int b = 1000; b >= -1000; --b)
            {
                if (!PrimeUtilities.IsPrime(b))
                {
                    continue;
                }

                for (int a = -1000; a < 1000; ++a)
                {
                    if (!PrimeUtilities.IsPrime(1 + a + b))
                    {
                        continue;
                    }

                    // Check length of consecutive prime list.
                    int n = 2;
                    while (PrimeUtilities.IsPrime((n * n) + (a * n) + b))
                    {
                        ++n;
                    }

                    if (n > consecutivePrimeCount)
                    {
                        consecutivePrimeCount = n;

                        bestA = a;
                        bestB = b;
                    }
                }
            }

            return bestA * bestB;
        }

        // Sum of the diagonals in a 1001x1001 spiral, formed by starting with one and moving in a clockwise direction
        // 7 8 9
        // 6 1 2
        // 5 4 3
        private static int Problem28()
        {
            const int squareWidth = 1001;
            int sum = 1;

            int bottomRightNumber = 3;

            for (int i = 2; i < squareWidth; i += 2)
            {
                sum += (4 * bottomRightNumber) + (6 * i);
                bottomRightNumber += 2 + (i / 2) * 8;
            }

            return sum;
        }

        // How many distinct terms are in the sequence generated by a^b for 2 <= a <= 100, 2 <= b <= 100.
        private static int Problem29()
        {
            const int limit = 100;
            var sequence = new HashSet<BigInteger>();

            for (int a = 2; a <= limit; ++a)
            {
                for (int b = 2; b <= limit; ++b)
                {
                    sequence.Add(BigInteger.Pow(a, b));
                }
            }

            return sequence.Count;
        }

        // Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
        // ~100ms
        private static int Problem30()
        {
            int sum = 0;

            // Pretty abitrary limit here, 100,000 was too small.
            // I narrowed it down from 1,000,000.
            for (int i = 2; i < 200000; ++i)
            {
                if (i == Utility.SumOfPowerOfDigits(i, 5))
                {
                    sum += i;
                }
            }

            return sum;
        }
    }
}
