using System.Collections.Generic;

namespace Guestbook.Application.Models
{
    public record GuestbookModel(IEnumerable<GuestbookEntry> Entries);
}
