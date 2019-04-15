﻿using System;
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

        public BookmarksController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public Bookmark GetBookmark(int id)
        {
            return _bookmarkRepository.GetBookmark(id);
        }

        //[JwtAuthentication]
        [HttpPost]
        public Bookmark CreateBookmark(CreateJson content)
        {
            return _bookmarkRepository.CreateBookmark(content.Bookmark, content.Username);
        }

        //[JwtAuthentication]
        [HttpPut]
        public HttpStatusCode EditBookmark(EditJson content)
        {
            return _bookmarkRepository.EditBookmark(content.UserId, content.Bookmark);
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
        [HttpPost]
        public IEnumerable<User> FavouriteBookmark(int bookmarkId,int userId)
        {
            return _bookmarkRepository.FavouriteBookmark(bookmarkId, userId);
        }

        //add tests
        //add routing 
        [HttpGet]
        public IEnumerable<Bookmark> SearchBookmarks(string title)
        {
            return _bookmarkRepository.SearchBookmarks(title);
        }

    }
}