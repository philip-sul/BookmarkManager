using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookmarkManager.Models;
using Lab6.Filters;

namespace BookmarkManager.Controllers
{
    public class BookmarksController : ApiController
    {
        private IBookmarkRepository _bookmarkRepository;

        public BookmarksController()
        {
            _bookmarkRepository = new BookmarkRepository();
        }

        [AllowAnonymous]
        [HttpGet]
        public Bookmark GetBookmark(int id)
        {
            return _bookmarkRepository.GetBookmark(id);
        }

        //[JwtAuthentication]
        [HttpPost]
        public Bookmark CreateBookmark(BookmarkClass1 content)
        {
            return _bookmarkRepository.CreateBookmark(content.Bookmark1, content.Username1);
        }

        //[JwtAuthentication]
        [HttpPut]
        public HttpStatusCode EditBookmark(int id, Bookmark bookmark, string username)
        {
            return _bookmarkRepository.EditBookmark(id, bookmark, username);
        }

        //[JwtAuthentication]
        [HttpDelete]
        public HttpStatusCode DeleteBookmark(int id)
        {
            return _bookmarkRepository.DeleteBookmark(id);
        }

        [AllowAnonymous]
        [HttpGet]
        public Bookmark GetBookmark(string title)
        {
            return _bookmarkRepository.GetBookmark(title);
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> GetBookmarkedUsers(int id)
        {
            return _bookmarkRepository.GetBookmarkedUsers(id);
        }

    }
}
