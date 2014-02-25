using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.MultipleEventHandlers
{
    public class MultipleImplementationEventHandler : IIndexingGenericEventHandler<string>, IIndexingGenericEventHandler<Status>, ILogEventHandler
    {
        public static int StatusCallcount = 0;
        public static int StringCallcount = 0;
        public static int LogCallcount = 0;

        public void AddedToIndex(Status item)
        {
            StatusCallcount++;
        }

        public void AddedToIndex(string item)
        {
            StringCallcount++;
        }

        public void Log(string message)
        {
            LogCallcount++;
        }
    }
}
