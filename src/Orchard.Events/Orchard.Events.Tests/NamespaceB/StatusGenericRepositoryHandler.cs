using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events.Tests.NamespaceB
{
    public class StatusGenericRepositoryHandler : IGenericRepositoryHandler<StatusEntity>
    {
        public static StatusEntity AddedEntity;
        public static int CallCount;

        public void EntityAdded(StatusEntity entity)
        {
            AddedEntity = entity;
            CallCount++;
        }
    }

    public class StatusEntity
    {
        public string Status { get; set; }
    }
}
