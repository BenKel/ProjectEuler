using System;
using ProjectEuler.Problems;

namespace ProjectEuler.App
{
    public class ProblemInstantiator : IProblemInstantiator
    {
        private readonly IServiceProvider _serviceProvider;

        public ProblemInstantiator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProblem GetProblemInstance(int problemNumber)
        {
            var problemClassName = $"{typeof(IProblem).Namespace}.Problem{problemNumber}";

            try
            {
                var problemType = typeof(IProblem).Assembly.GetType(problemClassName);
                var instance = (IProblem)_serviceProvider.GetService(problemType);

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