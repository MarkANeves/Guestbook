using System.Threading.Tasks;
using Guestbook.Api.Models;

namespace Guestbook.Api.Storage
{
    public interface IGuestbookStorage
    {
        Task<GuestbookModel> GetGuestbook();
        Task AddEntry(GuestbookEntry entry);
        Task ClearGuestbook();
    }
}
