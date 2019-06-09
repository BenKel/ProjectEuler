using System;
using System.Diagnostics;
using ProjectEuler.Problems;

namespace ProjectEuler.App
{
    public class Application
    {
        public void Run()
        {
            var problem = GetProblemToSolve();

            if (problem == null)
            {
                Console.WriteLine($"There was a problem finding the problem");
                Console.Read();
                return;
            }

            var watch = new Stopwatch();

            Console.WriteLine($"Problem processing:");

            watch.Start();

            Console.WriteLine($"{problem.GetAnswer()}");

            watch.Stop();

            Console.WriteLine($"Elapsed Time: {watch.ElapsedMilliseconds} ms");

            Console.Read();
        }

        private IProblem GetProblemToSolve()
        {
            Console.WriteLine("Enter Problem Number:");
            var validInput = false;
            var userInput = string.Empty;

            while (!validInput)
            {
                userInput = Console.ReadLine();

                validInput = int.TryParse(userInput, out int _);

                if (!validInput)
                {
                    Console.WriteLine("Integer please.");
                }
            }
            

            var problemClassName = $"{typeof(IProblem).Namespace}.Problem{userInput}";
            try
            {
                var problemType = typeof(IProblem).Assembly.GetType(problemClassName);
                var instance = (IProblem)Activator.CreateInstance(problemType);

                return instance;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}