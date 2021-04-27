using System;
using System.Threading.Tasks;
using FluentAssertions;
using Guestbook.Api.Storage;
using NUnit.Framework;

namespace Guestbook.Api.Test.Storage
{
    public abstract class GuestbookStorageTests
    {
        private IGuestbookStorage _storage;

        protected abstract IGuestbookStorage CreateStorage();

        [SetUp]
        public void Setup()
        {
            _storage = CreateStorage();
        }

        [Test]
        public async Task CanReadWriteAndClear()
        {
            var now = DateTime.UtcNow;

            await _storage.AddEntry(new Models.GuestbookEntry("1", now));
            await _storage.AddEntry(new Models.GuestbookEntry("2", now));
            await _storage.AddEntry(new Models.GuestbookEntry("3", now));

            var guestbook = await _storage.GetGuestbook();

            guestbook.Entries.Should().HaveCount(3)
                .And.BeEquivalentTo(
                    new Models.GuestbookEntry("1", now),
                    new Models.GuestbookEntry("2", now),
                    new Models.GuestbookEntry("3", now)
                );

            await _storage.ClearGuestbook();

            var guestbookAfterClear = await _storage.GetGuestbook();

            guestbookAfterClear.Entries.Should().BeEmpty();
        }
    }
}
