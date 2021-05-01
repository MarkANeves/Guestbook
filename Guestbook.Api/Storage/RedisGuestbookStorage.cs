using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Guestbook.Api.Models;
using StackExchange.Redis;

namespace Guestbook.Api.Storage
{
    public class RedisGuestbookStorage : IGuestbookStorage
    {
        private const string GuestbookKey = "guestbook";

        private readonly IDatabase _database;
        private readonly uint _capacity;

        public RedisGuestbookStorage(IDatabase database, uint capacity)
        {
            _database = database;
            _capacity = capacity;
        }

        public static async Task<RedisGuestbookStorage> Create(string connectionString, uint capacity)
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);
            return new RedisGuestbookStorage(redis.GetDatabase(), capacity);
        }

        public async Task<GuestbookModel> GetGuestbook()
        {
            var entriesValues = await _database.ListRangeAsync(GuestbookKey, 0, 99);

            var entries = entriesValues.Select(DeserializeEntry).ToArray();

            return new GuestbookModel(entries);
        }

        public async Task AddEntry(GuestbookEntry entry)
        {
            var length = await _database.ListLeftPushAsync(GuestbookKey, SerializeEntry(entry));

            // If we've exceeded the maximum capacity then trim the excess elements.
            // This solution may temporarily exceed the capacity, but is pragmatic in that it does not require
            // the complexity of transactions
            if (length > _capacity)
            {
                await _database.ListTrimAsync(GuestbookKey, 0, _capacity - 1);
            }

        }

        public Task ClearGuestbook()
        {
            return _database.KeyDeleteAsync(GuestbookKey);
        }

        private RedisValue SerializeEntry(GuestbookEntry entry) => JsonSerializer.Serialize(entry);
        private GuestbookEntry DeserializeEntry(RedisValue entryJson) => JsonSerializer.Deserialize<GuestbookEntry>(entryJson);
    }
}
