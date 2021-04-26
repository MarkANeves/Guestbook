using System;

namespace Guestbook.Application.Models
{
    public record GuestbookEntry(string Message, DateTime Timestamp);
}