using System;
namespace AppWithEvents
{
    public class StatusRepository : IStatusEventHandler
    {
        public void UpdateStatus(int status)
        {
            Console.WriteLine("Repository: " + status);
        }

        public void OnStatusChanged(int newStatus)
        {
            UpdateStatus(newStatus);
        }
    }
}
