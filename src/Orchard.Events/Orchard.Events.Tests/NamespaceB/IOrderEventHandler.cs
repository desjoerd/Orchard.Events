using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.NamespaceB
{
    public interface IOrderEventHandler : IEventHandler
    {
        void OrderProcessed(int orderId);
    }
}
