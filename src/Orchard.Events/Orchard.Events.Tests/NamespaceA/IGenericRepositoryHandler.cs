using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.NamespaceA
{
    public interface IGenericRepositoryHandler<TEntity> : IEventHandler
    {
        void EntityAdded(TEntity entity);
    }
}
