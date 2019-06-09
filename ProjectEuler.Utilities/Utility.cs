using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Utilities
{
    internal static class Utility
    {
        private static Dictionary<long, int> collatzSequenceCache;
        private static Dictionary<int, int> numberLetterCount;

        public static int NumberOfDivisors(long number)
        {
            int numberOfDivisors = 1;

            List<int> primeFactors = PrimeUtilities.PrimeFactors(number);

            // number of divisors d(n) = (a+1)(b+1)(c+1)...
            // where n = p^a * q^b * r^c ... (expressed as a product of prime factors)

            while (primeFactors.Count > 0)
            {
                int a = primeFactors[0];
                primeFactors.RemoveAt(0);
                int countOfA = 1;

                // Remove all other occurences of a.
                while (true)
                {
                    if (primeFactors.Contains(a))
                    {
                        ++countOfA;
                        primeFactors.Remove(a);
                        continue;
                    }
                    break;
                }

                numberOfDivisors *= countOfA + 1;
            }

            return numberOfDivisors;
        }

        public static BigInteger NumberOfDivisors(BigInteger number)
        {
            BigInteger numberOfDivisors = BigInteger.One;

            List<BigInteger> primeFactors = PrimeUtilities.PrimeFactors(number);

            // number of divisors d(n) = (a+1)(b+1)(c+1)...
            // where n = p^a * q^b * r^c ... (expressed as a product of prime factors)

            while (primeFactors.Count > 0)
            {
                BigInteger a = primeFactors[0];
                primeFactors.RemoveAt(0);
                int countOfA = 1;

                // Remove all other occurences of a.
                while (true)
                {
                    if (primeFactors.Contains(a))
                    {
                        ++countOfA;
                        primeFactors.Remove(a);
                        continue;
                    }
                    break;
                }

                numberOfDivisors *= countOfA + 1;
            }

            return numberOfDivisors;
        }

        public static int SumOfPowerOfDigits(int number, int power)
        {
            int sum = 0;

            char[] digits = number.ToString().ToCharArray();

            foreach (char c in digits)
            {
                int digit = int.Parse(c.ToString());
                sum += (int)Math.Pow(digit, power);
            }

            return sum;
        }

        public static int CoinSum(int onePound, int fifty, int twenty, int ten, int five, int two, int one)
        {
            int sum = 0;

            sum += 100 * onePound;
            sum += 50 * fifty;
            sum += 20 * twenty;
            sum += 10 * ten;
            sum += 5 * five;
            sum += 2 * two;
            sum += one;

            return sum;
        }

        public static List<int> GetAbundantNumbers(int inclusiveLimit)
        {
            var abundantNumbers = new List<int>();

            for (int i = 12; i <= inclusiveLimit; ++i)
            {
                int factorSum = SumFactors(i);

                if (factorSum > i)
                {
                    abundantNumbers.Add(i);
                }
            }

            return abundantNumbers;
        }

        public static long SumUpTo(int inclusiveLimit)
        {
            long sum = 0;

            for (int i = 1; i <= inclusiveLimit; ++i)
            {
                sum += i;
            }

            return sum;
        }

        public static List<int> GetFactors(int number)
        {
            var factors = new List<int> { 1 };

            for (int i = 2; i * i <= number; ++i)
            {
                if (number % i != 0) continue;

                factors.Add(i);

                // Don't add the square root twice.
                if (i != number / i)
                {
                    factors.Add(number / i);
                }
            }
            return factors;
        }

        public static int SumFactors(int number)
        {
            List<int> factors = GetFactors(number);

            return factors.Sum();
        }

        public static int CollatzSequenceLength(long number)
        {
            if (collatzSequenceCache == null)
            {
                collatzSequenceCache = new Dictionary<long, int>();
            }

            if (number <= 0)
            {
                throw new ArgumentException("number must be a positive integer");
            }

            if (number == 1)
            {
                return 1;
            }

            if (collatzSequenceCache.ContainsKey(number))
            {
                return collatzSequenceCache[number];
            }

            int seqLength;

            if (number % 2 == 0)
            {
                seqLength = CollatzSequenceLength(number / 2);
            }
            else
            {
                seqLength = CollatzSequenceLength(3 * number + 1);
            }

            collatzSequenceCache.Add(number, seqLength + 1);

            return seqLength + 1;
        }

        public static int CountLettersInNumber(int number)
        {
            if (number == 1000)
            {
                return 8;
            }

            if (number > 1000)
            {
                throw new ArgumentException("number too big");
            }

            #region Word length dictionary init

            if (numberLetterCount == null)
            {
                numberLetterCount = new Dictionary<int, int>
                {
                    {1, 3},
                    {2, 3},
                    {3, 5},
                    {4, 4},
                    {5, 4},
                    {6, 3},
                    {7, 5},
                    {8, 5},
                    {9, 4},
                    {10, 3},
                    {11, 6},
                    {12, 6},
                    {13, 8},
                    {14, 8},
                    {15, 7},
                    {16, 7},
                    {17, 9},
                    {18, 8},
                    {19, 8},
                    {20, 6},
                    {30, 6},
                    {40, 5},
                    {50, 5},
                    {60, 5},
                    {70, 7},
                    {80, 6},
                    {90, 6},
                    {100, 7}
                };
            }

            #endregion Word length dictionary init

            if (number <= 20)
            {
                return numberLetterCount[number];
            }

            if (number < 100)
            {
                if (number % 10 == 0)
                {
                    return numberLetterCount[number];
                }
                // Use integer division to get the tens value. i.e. 43 / 10 = 4.
                // 4 * 10 = 40.
                var tens = numberLetterCount[number / 10 * 10];
                var ones = numberLetterCount[number % 10];
                var count = tens + ones;
                return count;
            }

            if (number % 100 == 0)
            {
                // Add on 'hundred' + 'one'/'two' etc.
                return numberLetterCount[100] + numberLetterCount[number / 100];
            }

            // Count the hundred part, then the tens part, then add 'and'
            var hundreds = CountLettersInNumber(number / 100 * 100);
            var remains = CountLettersInNumber(number % 100);
            return hundreds + remains + 3;
        }

        public static BigInteger NFactorial(int n)
        {
            BigInteger factorial = 1;

            for (int i = 2; i <= n; ++i)
            {
                factorial = BigInteger.Multiply(factorial, i);
            }

            return factorial;
        }

        // From stackoverflow
        public static bool NextPermutation(List<char> numList)
        {
            /*
             Knuths
             1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
             2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
             3. Swap a[j] with a[l].
             4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

             */
            var largestIndex = -1;
            for (var i = numList.Count - 2; i >= 0; i--)
            {
                if (numList[i] < numList[i + 1])
                {
                    largestIndex = i;
                    break;
                }
            }

            if (largestIndex < 0) return false;

            var largestIndex2 = -1;
            for (var i = numList.Count - 1; i >= 0; i--)
            {
                if (numList[largestIndex] < numList[i])
                {
                    largestIndex2 = i;
                    break;
                }
            }

            var tmp = numList[largestIndex];
            numList[largestIndex] = numList[largestIndex2];
            numList[largestIndex2] = tmp;

            for (int i = largestIndex + 1, j = numList.Count - 1; i < j; i++, j--)
            {
                tmp = numList[i];
                numList[i] = numList[j];
                numList[j] = tmp;
            }

            return true;
        }

        public static string GetRecurringDecimal(int numerator, int denominator)
        {
            decimal dec = (decimal)numerator / denominator;
            string decString = dec.ToString(CultureInfo.InvariantCulture);
            int maxRepeatLength = denominator - 1;

            if (decString.Length < 2 * maxRepeatLength)
            {
                return string.Empty;
            }

            string repeat = "";
            int index = 0;

            // Move the index after the decimal point.
            while (!decString[index].Equals('.'))
            {
                ++index;
            }
            ++index;

            decString = decString.Substring(index);

            for (int repeatLength = 1; repeatLength < maxRepeatLength; ++repeatLength)
            {
                bool foundRepeat = true;
                int offset = 0;

                for (index = 0; index < maxRepeatLength && index + repeatLength < decString.Length; ++index)
                {
                    if (decString[index + offset] != decString[index + repeatLength + offset])
                    {
                        index = -1;
                        ++offset;
                    }

                    if (offset >= maxRepeatLength)
                    {
                        foundRepeat = false;
                        break;
                    }
                }

                if (foundRepeat)
                {
                    repeat = decString.Substring(offset, repeatLength);
                    break;
                }
            }

            return repeat;
        }

        public static string GetRecurringDecimal2(int numerator, int denominator)
        {
            int maxRepeatLength = denominator - 1;
            var pow = BigInteger.Pow(10, maxRepeatLength);
            var bigNumer = BigInteger.Multiply(new BigInteger(numerator), pow);
            var length = bigNumer.ToString().Length;
            BigInteger decimalRepresentation = BigInteger.Divide(bigNumer, denominator);
            string decString = decimalRepresentation.ToString(CultureInfo.InvariantCulture);

            if (decString.Length < maxRepeatLength / 2)
            {
                return string.Empty;
            }

            string repeat = "";

            for (int repeatLength = 1; repeatLength < maxRepeatLength; ++repeatLength)
            {
                bool foundRepeat = true;
                int offset = 0;

                for (int index = 0; index < maxRepeatLength && index + repeatLength + offset < decString.Length; ++index)
                {
                    if (decString[index + offset] != decString[index + repeatLength + offset])
                    {
                        index = -1;
                        ++offset;
                    }

                    if (offset >= maxRepeatLength / 2)
                    {
                        foundRepeat = false;
                        break;
                    }
                }

                if (foundRepeat)
                {
                    repeat = decString.Substring(offset, repeatLength);
                    break;
                }
            }

            return repeat;
        }

        public static int[,] ParseFileToIntArray(string filePath, int arrayWidth, int arrayHeight)
        {
            var array = new int[arrayWidth, arrayHeight];
            int index0 = 0;
            int index1 = 0;

            using (var reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    string number = null;
                    foreach (char c in line)
                    {
                        if (c.Equals(' '))
                        {
                            if (number != null)
                            {
                                array[index0, index1] = Int32.Parse(number);
                                ++index1;
                            }

                            number = null;
                        }
                        else
                        {
                            number += c;
                        }
                    }

                    array[index0, index1] = Int32.Parse(number);
                    index1 = 0;
                    ++index0;

                    line = reader.ReadLine();
                }
            }

            return array;
        }

        public static List<string> ParseFileToStringList(string filePath)
        {
            var list = new List<string>();

            using (var reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    string entry = null;
                    foreach (char c in line)
                    {
                        if (c.Equals('"'))
                        {
                            if (entry != null)
                            {
                                list.Add(entry);
                            }

                            entry = null;
                        }
                        else if (c != '"' && c != ',')
                        {
                            entry += c;
                        }
                    }

                    if (entry != null)
                    {
                        list.Add(entry);
                    }

                    line = reader.ReadLine();
                }
            }

            return list;
        }

        public static IEnumerable<string> YieldParseFileToStringList(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    string entry = null;
                    foreach (char c in line)
                    {
                        if (c.Equals('"'))
                        {
                            if (entry != null)
                            {
                                yield return entry;
                            }

                            entry = null;
                        }
                        else if (c != '"' && c != ',')
                        {
                            entry += c;
                        }
                    }

                    if (entry != null)
                    {
                        yield return entry;
                    }

                    line = reader.ReadLine();
                }
            }
        }

        public static bool AnyDuplicates<T>(List<T> list)
        {
            return list.Count != list.Distinct().Count();
        }
    }
}