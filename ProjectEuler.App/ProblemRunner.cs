using System;
using System.Diagnostics;
using ProjectEuler.Problems;

namespace ProjectEuler.App
{
    public class ProblemRunner : IProblemRunner
    {
        private readonly IProblemInstantiator _problemInstantiator;

        public ProblemRunner(IProblemInstantiator problemInstantiator)
        {
            _problemInstantiator = problemInstantiator;
        }

        public void Run()
        {
            var problem = GetProblemToSolve();

            if (problem == null)
            {
                Console.WriteLine("There was a problem finding the problem");
                Console.Read();
                return;
            }

            Console.WriteLine(problem.Title);
            Console.WriteLine(problem.Description);

            var watch = new Stopwatch();

            Console.WriteLine("Problem processing");

            string answer;

            watch.Start();

            answer = problem.GetAnswer();

            watch.Stop();

            Console.WriteLine("Answer: ");
            Console.WriteLine(answer);
            Console.WriteLine($"Elapsed Time: {watch.ElapsedMilliseconds} ms");

            Console.Read();
        }

        private IProblem GetProblemToSolve()
        {
            Console.WriteLine("Enter Problem Number:");
            var validInput = false;
            var parsedProblemNumber = 0;

            while (!validInput)
            {
                var userInput = Console.ReadLine();

                validInput = int.TryParse(userInput, out parsedProblemNumber);

                if (!validInput)
                {
                    Console.WriteLine("Integer please.");
                }
            }

            return _problemInstantiator.GetProblemInstance(parsedProblemNumber);
        }
    }
}