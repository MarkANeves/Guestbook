using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Guestbook.Application.Models;
using StackExchange.Redis;

namespace Guestbook.Application.Storage
{
    public class RedisGuestbookStorage : IGuestbookStorage
    {
        private const string GuestbookKey = "guestbook";

        private readonly IDatabase _database;

        public RedisGuestbookStorage(IDatabase database)
        {
            _database = database;
        }

        public static async Task<RedisGuestbookStorage> Create(string connectionString)
        {
            var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);
            return new RedisGuestbookStorage(redis.GetDatabase());
        }

        public async Task<GuestbookModel> GetGuestbook()
        {
            var entriesValues = await _database.ListRangeAsync(GuestbookKey, 0, 99);

            var entries = entriesValues.Select(DeserializeEntry).ToArray();

            return new GuestbookModel(entries);
        }

        public Task AddEntry(GuestbookEntry entry)
        {
            return _database.ListLeftPushAsync(GuestbookKey, SerializeEntry(entry));
        }

        public Task ClearGuestbook()
        {
            return _database.KeyDeleteAsync(GuestbookKey);
        }

        private RedisValue SerializeEntry(GuestbookEntry entry) => JsonSerializer.Serialize(entry);
        private GuestbookEntry DeserializeEntry(RedisValue entryJson) => JsonSerializer.Deserialize<GuestbookEntry>(entryJson);
    }
}
