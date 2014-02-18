namespace Orchard.Events.Tests
{
    public interface ITestEventHandler : IEventHandler
    {
        void OnEvent(string data);
    }
}
