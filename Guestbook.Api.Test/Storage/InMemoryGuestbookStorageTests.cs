using Guestbook.Api.Storage;

namespace Guestbook.Api.Test.Storage
{
    public class InMemoryGuestbookStorageTests : GuestbookStorageTests
    {
        protected override IGuestbookStorage CreateStorage() => new InMemoryGuestbookStorage();
    }
}
