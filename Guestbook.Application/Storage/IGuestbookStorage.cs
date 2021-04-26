using System.Threading.Tasks;
using Guestbook.Application.Models;

namespace Guestbook.Application.Storage
{
    public interface IGuestbookStorage
    {
        Task<GuestbookModel> GetGuestbook();
        Task AddEntry(GuestbookEntry entry);
        Task ClearGuestbook();
    }
}
