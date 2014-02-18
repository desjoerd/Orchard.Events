using System;
namespace AppWithEvents
{
    public class LogStatusEventHandler : IStatusEventHandler
    {
        public void OnStatusChanged(int newStatus)
        {
            Console.WriteLine("Log: " + newStatus);
        }
    }
}
