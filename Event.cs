using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyEvents
{
    class Event
    {
        public int ID;
        public string Name;
        public string Description;
        public string Date;
        public int VenueID;
        public int EventPrice; // Added an extra property

        public Chat chat = new Chat();

    }
}
