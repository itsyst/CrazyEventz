using System;

namespace CrazyEvents
{
    /// <summary>
    /// Class representation of table database
    /// </summary>
    public class Chat
    {

        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

    }
}