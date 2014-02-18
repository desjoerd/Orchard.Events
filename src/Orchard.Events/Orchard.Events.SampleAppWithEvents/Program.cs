using Autofac;
using Orchard.Events;
using System;
namespace AppWithEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();

            var statusEventHandler = container.Resolve<IStatusEventHandler>();

            statusEventHandler.OnStatusChanged(100);
            statusEventHandler.OnStatusChanged(200);

            Console.Read();
        }

        static ILifetimeScope BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<EventsModule>();

            // RegisterType generic
            containerBuilder.RegisterType<LogStatusEventHandler>().AsEventHandler();

            // Or RegisterType non-generic
            containerBuilder.RegisterType(typeof(LogStatusEventHandler)).AsEventHandler(typeof(LogStatusEventHandler));
            
            // RegisterInstance
            var repoInstance = new StatusRepository();
            containerBuilder.RegisterInstance(repoInstance).AsEventHandler();

            return containerBuilder.Build();
        }
    }
}
