using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.NamespaceB
{
    public class TestOrderEventHandler : IOrderEventHandler
    {
        public static int CallCount = 0;
        public void OrderProcessed(int orderId)
        {
            CallCount++;
        }
    }
}
