using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Orchard.Events.Tests
{
    [TestClass]
    public class EventTests : ITestEventHandler
    {
        private ILifetimeScope _container;
        private TestEventHandler _testEventHandler;
        private int _onEventCalledTimes;

        [TestInitialize]
        public void Initialize()
        {
            _onEventCalledTimes = 0;
            TestEventHandler.CallCount = 0;

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterInstance(this)
                .AsEventHandler();

            _testEventHandler = new TestEventHandler();

            containerBuilder
                .RegisterInstance(_testEventHandler)
                .AsEventHandler();

            containerBuilder
                .RegisterType<TestEventHandler>()
                .As<TestEventHandler>()
                .AsEventHandler()
                .SingleInstance();

            containerBuilder
                .RegisterType<StringGenericEventHandler>()
                .AsEventHandler()
                .InstancePerDependency();

            containerBuilder.RegisterType<IntGenericEventHandler>()
                .AsEventHandler()
                .SingleInstance();

            containerBuilder
                .RegisterType(typeof(TestEventHandler))
                .AsEventHandler(typeof(TestEventHandler));

            containerBuilder.RegisterModule<EventsModule>();

            this._container = containerBuilder.Build();
        }

        [TestMethod]
        public void TestEventHandlerCalls()
        {
            var testEventHandlerProxy = _container.Resolve<ITestEventHandler>();
            testEventHandlerProxy.OnEvent("test");

            Assert.IsTrue(_testEventHandler.OnEventIsCalled);
            Assert.IsTrue(OnEventIsCalled);

            var testEventHandlerProxy2 = _container.Resolve<ITestEventHandler>();
            testEventHandlerProxy2.OnEvent("test2");

            var lifetime = _container.BeginLifetimeScope();

            var testEventHandlerProxy3 = lifetime.Resolve<ITestEventHandler>();
            testEventHandlerProxy3.OnEvent("test3");

            Assert.AreEqual(3, _testEventHandler.OnEventCalledTimes);
            Assert.AreEqual(3, _onEventCalledTimes);

            var testEventHandlerSingleton = _container.Resolve<TestEventHandler>();

            Assert.AreEqual(3, testEventHandlerSingleton.OnEventCalledTimes);

            Assert.AreEqual(9, TestEventHandler.CallCount);
        }

        [TestMethod]
        public void Speed()
        {
            TestEventHandlerCalls();
        }

        [TestMethod]
        public void TestGeneric()
        {
            StringGenericEventHandler.CallCount = 0;
            IntGenericEventHandler.CallCount = 0;

            var stringGenericHandlerProxy = _container.Resolve<IGenericEventHandler<string>>();
            stringGenericHandlerProxy.TestGeneric();

            Assert.AreEqual(1, StringGenericEventHandler.CallCount);
            Assert.AreEqual(0, IntGenericEventHandler.CallCount);

            var intGenericHandlerProxy = _container.Resolve<IGenericEventHandler<int>>();
            intGenericHandlerProxy.TestGeneric();

            Assert.AreEqual(1, StringGenericEventHandler.CallCount);
            Assert.AreEqual(1, IntGenericEventHandler.CallCount);
        }

        public void OnEvent(string data)
        {
            _onEventCalledTimes++;
        }

        public bool OnEventIsCalled { get { return _onEventCalledTimes > 0; } }
    }
}
