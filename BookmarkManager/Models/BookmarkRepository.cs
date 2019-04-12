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

        HttpStatusCode EditBookmark(int id, Bookmark bookmark);

        HttpStatusCode DeleteBookmark(int id);

        Bookmark GetBookmark(string title);

        IEnumerable<User> GetBookmarkedUsers(int id);


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
            if (id == null)
                throw new HttpRequestException("Id is required");

            var bookmark = _db.Bookmarks.Find(id);

            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return bookmark;
        }

        public Bookmark CreateBookmark(Bookmark bookmark, string username)//
        {
            //link, title, date, author, username?
            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            _db.Bookmarks.Add(bookmark);

            var user =_db.Users.SingleOrDefault(x => x.Username.ToLower() == username.ToLower());

            if(user == null)
                throw new HttpRequestException("Username not found");

            user.Bookmarks.Add(bookmark);

            _db.SaveChanges();

            return bookmark;
        }

        public HttpStatusCode EditBookmark(int id, Bookmark bookmark)//
        {
            //pass info to edit? or bookmark as well

            //link, title, date, author, username?

            var bookmarkToChange =_db.Bookmarks.Find(id);

            if (bookmarkToChange == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            bookmarkToChange.Link = bookmark.Link;

            if(bookmarkToChange.Title != null)
                bookmarkToChange.Title = bookmark.Title;

            bookmarkToChange.Date = bookmark.Date;

            bookmarkToChange.AuthorId = bookmark.AuthorId;

            _db.SaveChanges();

            return HttpStatusCode.Accepted;
        }

        public HttpStatusCode DeleteBookmark(int id)
        {
            var bookmark = _db.Bookmarks.Find(id);

            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            _db.Bookmarks.Remove(bookmark);//need to .clear()?

            _db.SaveChanges();

            return HttpStatusCode.Accepted;
        }

        public Bookmark GetBookmark(string title)
        {
            var bookmark = _db.Bookmarks.SingleOrDefault(x => x.Title.ToLower() == title.ToLower());

            if(bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return new Bookmark();
        }

        public IEnumerable<User> GetBookmarkedUsers(int id)//
        {

            var list = _db.Bookmarks.Find(id).Favourites;


            if(list == null)
                throw new HttpRequestException("Id not found");

            return list;

        }

    }
}