using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Guestbook.Api.Models;

namespace Guestbook.Api.Storage
{
    public class InMemoryGuestbookStorage : IGuestbookStorage
    {
        private readonly uint _capacity;
        private readonly ConcurrentQueue<GuestbookEntry> _entries;
        private readonly SemaphoreSlim _addEntrySemaphore;

        public InMemoryGuestbookStorage(uint capacity)
        {
            _capacity = capacity;
            _entries = new ConcurrentQueue<GuestbookEntry>();
            _addEntrySemaphore = new(1, 1);
        }

        public Task<GuestbookModel> GetGuestbook()
        {
            var orderedEntries = Enumerable.Reverse(_entries).ToArray();
            return Task.FromResult(new GuestbookModel(orderedEntries));
        }

        public async Task AddEntry(GuestbookEntry entry)
        {
            await _addEntrySemaphore.WaitAsync();

            try
            {
                // Dequeue entries until we have capacity for the new entry
                while (_entries.Count >= _capacity)
                {
                    _entries.TryDequeue(out _);
                }

                _entries.Enqueue(entry);
            }
            finally
            {
                _addEntrySemaphore.Release();
            }
        }

        public Task ClearGuestbook()
        {
            _entries.Clear();
            return Task.CompletedTask;
        }
    }
}
