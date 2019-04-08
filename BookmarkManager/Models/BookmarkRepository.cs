using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BookmarkManager.Models
{
    public interface IBookmarkRepository
    {
        Bookmark GetBookmark(int id);
 
        Bookmark CreateBookmark(User user);

        HttpStatusCode EditBookmark(int id);

        HttpStatusCode SaveBookmark();

        HttpStatusCode DeleteBookmark(int id);

        Bookmark GetBookmark(string title);

        IEnumerable<User> GetBookmarks(User user);


    }

    public class BookmarkRepository : IBookmarkRepository
    {
        private DatabaseModel _db;

        public BookmarkRepository()
        {
            _db = new DatabaseModel();
        }

        public Bookmark GetBookmark(int id)
        {
            return new Bookmark();
        }

        public Bookmark CreateBookmark(User user)
        {
            return new Bookmark();
        }

        public HttpStatusCode EditBookmark(int id)
        {
            return HttpStatusCode.Accepted;
        }

        public HttpStatusCode SaveBookmark()
        {
            return HttpStatusCode.Accepted;
        }

        public HttpStatusCode DeleteBookmark(int id)
        {
            return HttpStatusCode.Accepted;
        }

        public Bookmark GetBookmark(string title)
        {
            return new Bookmark();
        }

        public IEnumerable<User> GetBookmarks(User user)
        {
            return new List<User>();
        }

    }
}