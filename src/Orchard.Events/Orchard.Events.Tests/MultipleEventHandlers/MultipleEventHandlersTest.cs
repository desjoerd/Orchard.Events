using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Orchard.Events.Tests.MultipleEventHandlers
{
    [TestClass]
    public class MultipleEventHandlersTest
    {
        private ILifetimeScope _container;

        [TestInitialize]
        public void Initialize()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterType<MultipleImplementationEventHandler>()
                .AsEventHandler();

            containerBuilder.RegisterModule<EventsModule>();

            _container = containerBuilder.Build();
        }

        [TestMethod]
        public void TestMultipleEventHandlerImplementations()
        {
            MultipleImplementationEventHandler.StatusCallcount = 0;
            MultipleImplementationEventHandler.StringCallcount = 0;
            MultipleImplementationEventHandler.LogCallcount = 0;

            var stringIndexingHandlerProxy = _container.Resolve<IIndexingGenericEventHandler<string>>();
            var statusIndexingHandlerProxy = _container.Resolve<IIndexingGenericEventHandler<Status>>();
            var logHandlerProxy = _container.Resolve<ILogEventHandler>();

            stringIndexingHandlerProxy.AddedToIndex("test");

            Assert.AreEqual(1, MultipleImplementationEventHandler.StringCallcount);
            Assert.AreEqual(0, MultipleImplementationEventHandler.StatusCallcount);
            Assert.AreEqual(0, MultipleImplementationEventHandler.LogCallcount);

            statusIndexingHandlerProxy.AddedToIndex(new Status { Name = "Test " });

            Assert.AreEqual(1, MultipleImplementationEventHandler.StringCallcount);
            Assert.AreEqual(1, MultipleImplementationEventHandler.StatusCallcount);
            Assert.AreEqual(0, MultipleImplementationEventHandler.LogCallcount);

            logHandlerProxy.Log("test");

            Assert.AreEqual(1, MultipleImplementationEventHandler.StringCallcount);
            Assert.AreEqual(1, MultipleImplementationEventHandler.StatusCallcount);
            Assert.AreEqual(1, MultipleImplementationEventHandler.LogCallcount);
        }

        [TestMethod]
        public void Speed()
        {
            TestMultipleEventHandlerImplementations();
        }
    }
}
