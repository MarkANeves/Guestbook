using System.Collections.Generic;
using System.Threading.Tasks;
using Guestbook.Api.Models;

namespace Guestbook.Api.Storage
{
    public class InMemoryGuestbookStorage : IGuestbookStorage
    {
        private readonly Stack<GuestbookEntry> _entries;

        public InMemoryGuestbookStorage()
        {
            _entries = new Stack<GuestbookEntry>();
        }

        public Task<GuestbookModel> GetGuestbook()
        {
            return Task.FromResult(new GuestbookModel(_entries));
        }

        public Task AddEntry(GuestbookEntry entry)
        {
            _entries.Push(entry);
            return Task.CompletedTask;
        }

        public Task ClearGuestbook()
        {
            _entries.Clear();
            return Task.CompletedTask;
        }
    }
}
