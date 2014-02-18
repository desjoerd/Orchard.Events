using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events
{
    public class EventsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new EventsRegistrationSource());

            builder.RegisterType<DefaultOrchardEventBus>()
                .As<IEventBus>();

            base.Load(builder);
        }
    }
}
