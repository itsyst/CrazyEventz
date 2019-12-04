using System;

namespace CrazyEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var crazyEvents = new EventAgency();
            crazyEvents.Start();
            Console.ReadLine();

        }
    }
}
