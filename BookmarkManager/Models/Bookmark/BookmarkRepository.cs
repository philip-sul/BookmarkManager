﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Lab6;
using Lab6.Filters;

namespace BookmarkManager.Models
{
    /// <summary>
    /// BookmarkRepository
    /// Philip Sulinski - 4/14/2019
    /// 
    /// Performs business logic for use in BookmarkController. 
    /// Used with the IBookmarkRepository for dependency injection.
    /// Queries db with DatabaseModel
    /// 
    /// </summary>
    public class BookmarkRepository : IBookmarkRepository
    {
        private DatabaseModel _db;

        public BookmarkRepository()
        {
            _db = new DatabaseModel();
        }

        public Bookmark GetBookmark(int id)
        {
            var bookmark = _db.Bookmarks.Find(id);

            if (bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return bookmark;
        }

        public Bookmark CreateBookmark(Bookmark bookmark, string username)//
        {
            //link, title, date, author, username?
            if (bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            _db.Bookmarks.Add(bookmark);

            var user = _db.Users.SingleOrDefault(x => x.Username.ToLower() == username.ToLower());

            if (user == null)
                throw new HttpRequestException("Username not found");

            user.Bookmarks.Add(bookmark);

            Save();

            return bookmark;
        }

        public HttpStatusCode EditBookmark(int userId, Bookmark bookmark)//
        {
            //pass info to edit? or bookmark as well

            //link, title, date, author, username?

            var bookmarkToChange = _db.Bookmarks.Find(bookmark.BookmarkId);

            if (bookmarkToChange == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            bookmarkToChange.Link = bookmark.Link;

            if (bookmarkToChange.Title != null)
                bookmarkToChange.Title = bookmark.Title;

            bookmarkToChange.Date = bookmark.Date;

            bookmarkToChange.AuthorId = userId;

            _db.SaveChanges();

            return HttpStatusCode.Accepted;
        }

        public HttpStatusCode DeleteBookmark(int id)
        {
            
            var bookmark = _db.Bookmarks.Find(id);

            if (bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            _db.Bookmarks.Remove(bookmark);//need to .clear()?

            _db.SaveChanges();

            return HttpStatusCode.Accepted;
        }

        public Bookmark GetBookmark(string title)
        {
            var bookmark = _db.Bookmarks.SingleOrDefault(x => x.Title.ToLower() == title.ToLower());

            if (bookmark == null)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return bookmark;
        }

        public IEnumerable<User> FavouriteBookmark(int bookmarkId, int userId)
        {

            var bookmark = _db.Bookmarks.Find(bookmarkId);

            var user = _db.Users.Find(userId);


            if (bookmark == null || user == null)
                throw new HttpRequestException("Id not found");

            bookmark.Users.Add(user);

            _db.SaveChanges();

            bookmark = _db.Bookmarks.Find(bookmarkId);

            if (bookmark == null)
                throw new HttpRequestException("Id not found");

            return bookmark.Users;

        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Bookmark> SearchBookmarks(string title)
        {
            var bookmarks = from u in _db.Bookmarks
                            where u.Title.ToLower().Contains(title.ToLower())
                            select u;

            return bookmarks;
        }
    }
}