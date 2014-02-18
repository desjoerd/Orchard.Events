using Autofac;
using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events
{
    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<TLimit, ConcreteReflectionActivatorData, SingleRegistrationStyle> AsEventHandler<TLimit>(
            this IRegistrationBuilder<TLimit, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder) where TLimit : IEventHandler
        {
            AsEventHandlerImpl(builder, typeof(TLimit));
            return builder;
        }

        public static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> AsEventHandler(
            this IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder, Type serviceType)
        {
            AsEventHandlerImpl(builder, serviceType);
            return builder;
        }

        public static IRegistrationBuilder<TLimit, SimpleActivatorData, SingleRegistrationStyle> AsEventHandler<TLimit>(
            this IRegistrationBuilder<TLimit, SimpleActivatorData, SingleRegistrationStyle> builder) where TLimit : IEventHandler
        {
            AsEventHandlerImpl(builder, typeof(TLimit));
            return builder;
        }

        private static void AsEventHandlerImpl<TLimit, TActivatorData, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, Type serviceType)
        {
            var interfaces = serviceType.GetInterfaces().Where(t => t != typeof(IEventHandler) && typeof(IEventHandler).IsAssignableFrom(t)).ToList();
            if (interfaces.Count == 0)
            {
                throw new InvalidOperationException(string.Format("No interface found on type: {0} with IEventHandler as base", serviceType.FullName));
            }

            foreach (var t in interfaces)
            {
                builder.Named<IEventHandler>(t.Name);
            }
        }
    }
}
