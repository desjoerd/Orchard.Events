using Orchard.Events;
namespace AppWithEvents
{
    public interface IStatusEventHandler : IEventHandler
    {
        void OnStatusChanged(int newStatus);
    }
}
