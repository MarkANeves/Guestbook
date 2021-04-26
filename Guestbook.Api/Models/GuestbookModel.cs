using System.Collections.Generic;

namespace Guestbook.Api.Models
{
    public record GuestbookModel(IEnumerable<GuestbookEntry> Entries);
}
