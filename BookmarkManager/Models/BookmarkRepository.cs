using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Lab6.Filters;

namespace BookmarkManager.Models
{
    public interface IBookmarkRepository
    {
        Bookmark GetBookmark(int id);
 
        Bookmark CreateBookmark(Bookmark bookmark, string username);

        HttpStatusCode EditBookmark(int id, Bookmark bookmark, string username);

        HttpStatusCode DeleteBookmark(int id);

        Bookmark GetBookmark(string title);

        IEnumerable<User> GetBookmarkedUsers(User user);


    }

    public class BookmarkRepository : IBookmarkRepository
    {
        private DatabaseModel _db;

        public BookmarkRepository()
        {
            _db = new DatabaseModel();
        }

        [AllowAnonymous]
        [HttpGet]
        public Bookmark GetBookmark(int id)
        {
            if (id == null)
                throw new HttpRequestException("Id is required");

            var bookmark = _db.Bookmarks.Find(id);

            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return bookmark;
        }

        [JwtAuthentication]
        [HttpPost]
        public Bookmark CreateBookmark(Bookmark bookmark, string username)//
        {
            //link, title, date, author, username?

            return new Bookmark();
        }

        [JwtAuthentication]
        [HttpPut]
        public HttpStatusCode EditBookmark(int id, Bookmark bookmark, string username)//
        {
            //pass info to edit? or bookmark as well

            //link, title, date, author, username?

            return HttpStatusCode.Accepted;
        }

        [JwtAuthentication]
        [HttpDelete]
        public HttpStatusCode DeleteBookmark(int id)
        {
            var bookmark = _db.Bookmarks.Find(id);

            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            _db.Bookmarks.Remove(bookmark);

            _db.SaveChanges();

            return HttpStatusCode.Accepted;
        }

        [AllowAnonymous]
        [HttpGet]
        public Bookmark GetBookmark(string title)
        {
            var bookmark = _db.Bookmarks.SingleOrDefault(x => x.Title.ToLower() == title.ToLower());

            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return new Bookmark();
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> GetBookmarkedUsers(User user)
        {
            //change to List<Bookmark> and do a where
            return new List<User>();
        }

    }
}