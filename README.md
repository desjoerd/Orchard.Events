# Orchard.Events
## A standalone implementation of the Orchard Eventbus.

### Dependencies
The Eventbus is tied to Autofac and Castle.DynamicProxy

### Introduction
Basicly this is the same eventbus implementation found in Orchard with some minor differences.

A good introduction about the Orchard Eventbus is found at: http://skywalkersoftwaredevelopment.net/orchard-development/api/ieventhandler

The big difference between the implementation of Orchard and the standalone implementation is the registration of EventHandlers.
In Orchard all EventHandlers are automaticly found because of the IDependency marker interface.
In this standalone implementation this must be done at the container builder.

### Usage
To start using the Orchard EventBus we need to register the EventsModule in the Autofac Container:
```csharp
using Autofac;
using Orchard.Events;
namespace AppWithEvents
{
    class Program
    {
        ...
        static ILifetimeScope BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<EventsModule>();

            return containerBuilder.Build();
        }
        ...
    }
}
```

After that define a interface for the EventHandler we want to use:
```csharp
using Orchard.Events;
namespace AppWithEvents
{
    public interface IStatusEventHandler : IEventHandler
    {
        void OnStatusChanged(int newStatus);
    }
}
```
We want two classes to listen to this event. The LogStatusEventHandler and a StatusRepository which uses the events to update the status.
```csharp
using System;
namespace AppWithEvents
{
    public class LogStatusEventHandler : IStatusEventHandler
    {
        public void OnStatusChanged(int newStatus)
        {
            Console.WriteLine("Log: " + newStatus);
        }
    }

    public class StatusRepository : IStatusEventHandler
    {
        public void UpdateStatus(int status)
        {
            Console.WriteLine("Repository: " + status); // Example implementation ;)
        }

        public void OnStatusChanged(int newStatus)
        {
            UpdateStatus(newStatus);
        }
    }
}
```
These eventHandler implementation must be registered at the container so we extend the BuildContainer() implementation:
```csharp
        ...
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
        ...
```
To raise the event we just resolve the IStatusEventHandler interface and call the event:
```csharp
        static void Main(string[] args)
        {
            var container = BuildContainer();

            var statusEventHandler = container.Resolve<IStatusEventHandler>();

            statusEventHandler.OnStatusChanged(100);
            statusEventHandler.OnStatusChanged(200);

            Console.Read();
        }
```
After we run this the output would be something like this:
```
Repository: 100
Log: 100
Log: 100
Repository: 200
Log: 200
Log: 200
```