using System;

namespace ProjectEuler.Problems
{
    public class Problem19 : ProblemBase
    {
        public override string GetAnswer()
        {
            int numberOfSundays = 0;

            for (var date = new DateTime(1901, 1, 1); date.Year < 2001; date = date.AddDays(1))
            {
                if (date.Day == 1 && date.DayOfWeek == DayOfWeek.Sunday)
                {
                    ++numberOfSundays;
                }
            }

            return numberOfSundays.ToString();
        }
    }
}