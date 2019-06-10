using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ProjectEuler.Problems;
using ProjectEuler.Utilities.Prime;

namespace ProjectEuler.App
{
    internal static class Program
    {
        private static IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            RegisterServices();

            var problemRunner = _serviceProvider.GetService<IProblemRunner>();
            problemRunner.Run();

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            var builder = new ContainerBuilder();

            builder.RegisterType<ProblemRunner>().As<IProblemRunner>();
            builder.RegisterType<ProblemInstantiator>().As<IProblemInstantiator>();
            builder.RegisterType<PrimeService>().As<IPrimeService>();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IProblem)))
                   .Where(t => t.Name.StartsWith("Problem"))
                   .AsSelf();

            builder.Populate(collection);

            var appContainer = builder.Build();

            _serviceProvider = new AutofacServiceProvider(appContainer);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }

            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}