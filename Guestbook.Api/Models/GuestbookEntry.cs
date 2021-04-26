using System;

namespace Guestbook.Api.Models
{
    public record GuestbookEntry(string Message, DateTime Timestamp);
}