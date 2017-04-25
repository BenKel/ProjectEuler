using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    // Returns the sum of all 0 to 9 pandigital numbers with the following property:
    // let d(1) be the first digit, d(2) the second etc.
    // d(2)d(3)d(4) % 2 == 0.
    // d(3)d(4)d(5) % 3 == 0.
    // d(4)d(5)d(6) % 5 == 0.
    // And so on for the next 4 primes.

    // Actually works pretty quickly. 
    internal class Problem43
    {
        private readonly List<string> twoMults;
        private readonly List<string> threeMults;
        private readonly List<string> fiveMults;
        private readonly List<string> sevenMults;
        private readonly List<string> elevenMults;
        private readonly List<string> thirteenMults;
        private readonly List<string> seventeenMults;

        public Problem43()
        {
            twoMults = new List<string>();
            threeMults = new List<string>();
            fiveMults = new List<string>();
            sevenMults = new List<string>();
            elevenMults = new List<string>();
            thirteenMults = new List<string>();
            seventeenMults = new List<string>();
        }

        public long GetAnswer()
        {
            // Works out all the 2 and 3 digit multiples of all primes up to 17. 
            // Then it builds the number, starting from the end, using the fact that the multiples overlap
            // to cancel out most of the possibilities.
            // Uses a bunch of nested loops, but very few iterations actually use all of the loops.
            long sum = 0;

            FillMultiplesArrays();

            // Can also be written as a crazy LINQ expression, but this is more readable to me.
            foreach (string sTeen in seventeenMults)
            {
                var thirteens = from mult in thirteenMults
                           where mult[2] == sTeen[1] && mult[1] == sTeen[0] 
                           select mult;

                foreach (string thTeen in thirteens)
                {
                    var elevens = from mult in elevenMults
                                    where mult[2] == thTeen[1] && mult[1] == thTeen[0]
                                    select mult;

                    foreach (string eleven in elevens)
                    {
                        var sevens = from mult in sevenMults
                                      where mult[2] == eleven[1] && mult[1] == eleven[0]
                                      select mult;

                        foreach (string seven in sevens)
                        {
                            var fives = from mult in fiveMults
                                          where mult[2] == seven[1] && mult[1] == seven[0]
                                          select mult;

                            foreach (string five in fives)
                            {
                                var threes = from mult in threeMults
                                              where mult[2] == five[1] && mult[1] == five[0]
                                              select mult;

                                foreach (string three in threes)
                                {
                                    var twos = from mult in twoMults
                                                  where mult[2] == three[1] && mult[1] == three[0]
                                                  select mult;

                                    foreach (string two in twos)
                                    {
                                        string fullNumber = two + seven + sTeen;

                                        if (StringUtilities.HasDuplicateChars(fullNumber))
                                        {
                                            continue;
                                        }

                                        fullNumber = AddMissingDigit(fullNumber);

                                        sum += long.Parse(fullNumber);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return sum;
        }

        private static string AddMissingDigit(string number)
        {
            if (!number.Contains("1"))
            {
                return 1 + number;
            }
            if (!number.Contains("2"))
            {
                return 2 + number;
            }
            if (!number.Contains("3"))
            {
                return 3 + number;
            }
            if (!number.Contains("4"))
            {
                return 4 + number;
            }
            if (!number.Contains("5"))
            {
                return 5 + number;
            }
            if (!number.Contains("6"))
            {
                return 6 + number;
            }
            if (!number.Contains("7"))
            {
                return 7 + number;
            }
            if (!number.Contains("8"))
            {
                return 8 + number;
            }
            if (!number.Contains("9"))
            {
                return 9 + number;
            }

            return number;
        }
        
        private void FillMultiplesArrays()
        {
            for (int i = 10; i <= 500; ++i)
            {
                int number = 2*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                twoMults.Add((i * 2).ToString("000"));
            }

            for (int i = 4; i <= 334; ++i)
            {
                int number = 3*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                threeMults.Add((3 * i).ToString("000"));
            }

            for (int i = 2; i <= 200; ++i)
            {
                int number = 5*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                fiveMults.Add((5 * i).ToString("000"));
            }

            for (int i = 2; i <= 143; ++i)
            {
                int number = 7*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                sevenMults.Add((7 * i).ToString("000"));
            }

            for (int i = 1; i <= 91; ++i)
            {
                int number = 11*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                elevenMults.Add((11 * i).ToString("000"));
            }

            for (int i = 1; i <= 77; ++i)
            {
                int number = 13*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                thirteenMults.Add((13 * i).ToString("000"));
            }

            for (int i = 1; i <= 59; ++i)
            {
                int number = 17*i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                seventeenMults.Add((17 * i).ToString("000"));
            }
        }
    }
}
