using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests
{
    public class TestEventHandler : ITestEventHandler
    {
        public static int CallCount = 0;

        public TestEventHandler()
        {
            OnEventData = new List<string>();
        }

        public void OnEvent(string data)
        {
            OnEventData.Add(data);
            CallCount++;
        }

        public List<string> OnEventData { get; private set; }

        public int OnEventCalledTimes { get { return OnEventData.Count; } }

        public bool OnEventIsCalled { get { return OnEventCalledTimes > 0; } }
    }
}
