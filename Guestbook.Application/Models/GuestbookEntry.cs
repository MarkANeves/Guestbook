using System;

namespace Guestbook.Application.Models
{
    public class GuestbookEntry
    {
        public string Message { get; init; }

        public DateTime Timestamp { get; init; }
    }
}