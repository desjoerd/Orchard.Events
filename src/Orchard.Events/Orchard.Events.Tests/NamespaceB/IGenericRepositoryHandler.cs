using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.NamespaceB
{
    public interface IGenericRepositoryHandler<TEntity> : IEventHandler
    {
        void EntityAdded(TEntity entity);
    }
}
