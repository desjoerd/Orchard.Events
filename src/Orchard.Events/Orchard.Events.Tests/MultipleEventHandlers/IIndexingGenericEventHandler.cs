using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.MultipleEventHandlers
{
    public interface IIndexingGenericEventHandler<T> : IEventHandler
    {
        void AddedToIndex(T item);
    }
}
