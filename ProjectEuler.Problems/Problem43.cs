using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem43 : ProblemBase
    {
        private List<string> _twoMults = new List<string>();
        private List<string> _threeMults = new List<string>();
        private List<string> _fiveMults = new List<string>();
        private List<string> _sevenMults = new List<string>();
        private List<string> _elevenMults = new List<string>();
        private List<string> _thirteenMults = new List<string>();
        private List<string> _seventeenMults = new List<string>();

        public override string Title => "Sub-string divisibility";

        public override string Description => @"
The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.

Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

    d2d3d4=406 is divisible by 2
    d3d4d5=063 is divisible by 3
    d4d5d6=635 is divisible by 5
    d5d6d7=357 is divisible by 7
    d6d7d8=572 is divisible by 11
    d7d8d9=728 is divisible by 13
    d8d9d10=289 is divisible by 17

Find the sum of all 0 to 9 pandigital numbers with this property.
            ";

        public override string GetAnswer()
        {
            // Works out all the 2 and 3 digit multiples of all primes up to 17.
            // Then it builds the number, starting from the end, using the fact that the multiples overlap
            // to cancel out most of the possibilities.
            // Uses a bunch of nested loops, but very few iterations actually use all of the loops.
            long sum = 0;

            FillMultiplesArrays();

            foreach (string sTeen in _seventeenMults)
            {
                var thirteens = from mult in _thirteenMults
                                where mult[2] == sTeen[1] && mult[1] == sTeen[0]
                                select mult;

                foreach (string thTeen in thirteens)
                {
                    var elevens = from mult in _elevenMults
                                  where mult[2] == thTeen[1] && mult[1] == thTeen[0]
                                  select mult;

                    foreach (string eleven in elevens)
                    {
                        var sevens = from mult in _sevenMults
                                     where mult[2] == eleven[1] && mult[1] == eleven[0]
                                     select mult;

                        foreach (string seven in sevens)
                        {
                            var fives = from mult in _fiveMults
                                        where mult[2] == seven[1] && mult[1] == seven[0]
                                        select mult;

                            foreach (string five in fives)
                            {
                                var threes = from mult in _threeMults
                                             where mult[2] == five[1] && mult[1] == five[0]
                                             select mult;

                                foreach (string three in threes)
                                {
                                    var twos = from mult in _twoMults
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

            return sum.ToString();
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
                int number = 2 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _twoMults.Add((i * 2).ToString("000"));
            }

            for (int i = 4; i <= 334; ++i)
            {
                int number = 3 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _threeMults.Add((3 * i).ToString("000"));
            }

            for (int i = 2; i <= 200; ++i)
            {
                int number = 5 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _fiveMults.Add((5 * i).ToString("000"));
            }

            for (int i = 2; i <= 143; ++i)
            {
                int number = 7 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _sevenMults.Add((7 * i).ToString("000"));
            }

            for (int i = 1; i <= 91; ++i)
            {
                int number = 11 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _elevenMults.Add((11 * i).ToString("000"));
            }

            for (int i = 1; i <= 77; ++i)
            {
                int number = 13 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _thirteenMults.Add((13 * i).ToString("000"));
            }

            for (int i = 1; i <= 59; ++i)
            {
                int number = 17 * i;
                if (StringUtilities.HasDuplicateChars(number.ToString()) || number >= 1000)
                {
                    continue;
                }

                _seventeenMults.Add((17 * i).ToString("000"));
            }
        }
    }
}