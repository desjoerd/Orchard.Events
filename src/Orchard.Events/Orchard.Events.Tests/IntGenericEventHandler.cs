using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests
{
    public class IntGenericEventHandler : IGenericEventHandler<int>
    {
        public static int CallCount = 0;

        public void TestGeneric()
        {
            CallCount++;
        }
    }
}
