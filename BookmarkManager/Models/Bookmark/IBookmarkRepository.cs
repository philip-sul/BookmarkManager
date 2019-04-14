using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkManager.Models
{
    public interface IBookmarkRepository
    {
        Bookmark GetBookmark(int id);
        Bookmark CreateBookmark(Bookmark bookmark, string username);
        HttpStatusCode EditBookmark(int userId, Bookmark bookmark);
        HttpStatusCode DeleteBookmark(int id);
        Bookmark GetBookmark(string title);
        IEnumerable<User> FavouriteBookmark(int bookmarkId, int userId);
        IEnumerable<Bookmark> SearchBookmarks(string title);
    }
}
