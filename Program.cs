using System;
using System.Diagnostics;

namespace ProjectEuler
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            var time = DateTime.Now.Ticks;

            var problem = new Problem51();
            Debug.WriteLine(problem.GetAnswer());

            var time2 = DateTime.Now.Ticks;

            Debug.WriteLine("Took " + (time2 - time) / TimeSpan.TicksPerMillisecond + " milliseconds");
        }
    }
}
