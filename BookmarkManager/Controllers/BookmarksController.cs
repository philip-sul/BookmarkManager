using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookmarkManager.Models;

namespace BookmarkManager.Controllers
{
    public class BookmarksController : ApiController
    {
        private IBookmarkRepository _bookmarkRepository;

        public BookmarksController()
        {
            _bookmarkRepository = new BookmarkRepository();
        }

        [HttpGet]
        public Bookmark GetBookmark(int id)
        {
            return _bookmarkRepository.GetBookmark(id);
        }

        public Bookmark CreateBookmark(Bookmark bookmark, string username)
        {
            return _bookmarkRepository.CreateBookmark(bookmark, username);
        }

        public HttpStatusCode EditBookmark(int id, Bookmark bookmark, string username)
        {
            return _bookmarkRepository.EditBookmark(id, bookmark, username);
        }

        public HttpStatusCode DeleteBookmark(int id)
        {
            return _bookmarkRepository.DeleteBookmark(id);
        }

        public Bookmark GetBookmark(string title)
        {
            return _bookmarkRepository.GetBookmark(title);
        }

        public IEnumerable<User> GetBookmarkedUsers(User user)
        {
            return _bookmarkRepository.GetBookmarkedUsers(user);
        }

    }
}
