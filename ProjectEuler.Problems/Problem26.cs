namespace ProjectEuler.Problems
{
    public class Problem26 : ProblemBase
    {
        public override string Title => "Reciprocal cycles";

        public override string Description => @"
A unit fraction contains 1 in the numerator. The decimal representation of the unit fractions with denominators 2 to 10 are given:

    1/2	= 	0.5
    1/3	= 	0.(3)
    1/4	= 	0.25
    1/5	= 	0.2
    1/6	= 	0.1(6)
    1/7	= 	0.(142857)
    1/8	= 	0.125
    1/9	= 	0.(1)
    1/10	= 	0.1

Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.

Find the value of d < 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
            ";

        public override string GetAnswer()
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

            return chosenDenominator.ToString();
        }
    }
}