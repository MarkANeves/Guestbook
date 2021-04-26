using System.Collections.Generic;

namespace Guestbook.Application.Models
{
    public class GuestbookModel
    {
        public IEnumerable<GuestbookEntry> Entries { get; init; }
    }
}
