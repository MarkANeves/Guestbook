using System;

namespace Guestbook.Application.Models
{
    public class GuestbookEntry
    {
        public string Message { get; }

        public DateTime Timestamp { get; }

        public GuestbookEntry(string message, DateTime timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }
    }
}