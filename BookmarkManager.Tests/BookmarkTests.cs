﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookmarkManager.Controllers;
using BookmarkManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookmarkManager.Tests
{
    [TestClass]
    public class BookmarkTests
    {
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetBookmark_NoId_ThrowException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call a bookmark that doesn't exist

            bookmarkRepo.Setup(x => x.GetBookmark(1)).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.GetBookmark(1);

            Assert.Fail();
        }

        [TestMethod]
        public void GetBookmark_WithIdSuccess_ReturnBookmark()
        {

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;

            //get a bookmark
            
            bookmarkRepo.Setup(x => x.GetBookmark(1)).Returns(new Bookmark{BookmarkId = 1});

            var result = controller.GetBookmark(1);

            Assert.AreEqual(bookmark.Object.BookmarkId, result.BookmarkId);


        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void CreateBookmark_BookmarkIsNull_ThrowsException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            bookmarkRepo.Setup(x => x.CreateBookmark(null, "test")).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.CreateBookmark(new CreateJson {Bookmark = null, Username = "test"});

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void CreateBookmark_UserIsNotFound_ThrowsException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmarkRepo.Setup(x => x.CreateBookmark(bookmark.Object, "test")).Throws(new HttpRequestException("Username not found"));

            //needs refactoring to test

            controller.CreateBookmark(new CreateJson {Bookmark = new Bookmark(), Username = "test2"});

            Assert.Fail();
        }

        [TestMethod]
        public void CreateBookmark_Success_ReturnBookmark()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmarkRepo.Setup(x => x.CreateBookmark(bookmark.Object, "test"));

            //bookmarkRepo.Verify(m => m.Save());

            controller.CreateBookmark(new CreateJson { Bookmark = new Bookmark(), Username = "test" });

            //test Save

            bookmarkRepo.Verify(m => m.Save()); 
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void EditBookmark_BookmarkIsNull_ThrowsException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            bookmarkRepo.Setup(x => x.EditBookmark(1,null)).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.EditBookmark(new EditJson{Bookmark = null,UserId = 1});

            Assert.Fail();
        }

        [TestMethod]
        public void EditBookmark_Success_ReturnBookmark()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;

            //get a bookmark

            //var result = controller.GetBookmark(It.IsAny<Int32>());

            bookmarkRepo.Setup(x => x.EditBookmark(1, bookmark.Object)).Returns(HttpStatusCode.Accepted);

            controller.EditBookmark(new EditJson { Bookmark = new Bookmark(), UserId = 1 });

            //test Save

            bookmarkRepo.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteBookmark_BookmarkNotFound_ThrowsException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmarkRepo.Setup(x => x.DeleteBookmark(1)).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.DeleteBookmark(1);

            Assert.Fail();
        }

        [TestMethod]
        public void DeleteBookmark_Success_ReturnHttpStatusCode()
        {

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            //controller.DeleteBookmark(1);

            //bookmarkRepo.Setup(x => x.CreateBookmark(new Bookmark{BookmarkId = 1}, "test"));

            bookmarkRepo.Setup(x => x.DeleteBookmark(1)).Returns(HttpStatusCode.Accepted);

            controller.DeleteBookmark(1);


            //test Save

            bookmarkRepo.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetBookmark_NoTitle_ThrowException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //var bookmark = new Mock<Bookmark>();

            //call a bookmark that doesn't exist

            bookmarkRepo.Setup(x => x.GetBookmark("test")).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.GetBookmark("test");

            Assert.Fail();

        }

        [TestMethod]
        public void GetBookmark_WithTitleSuccess_ReturnBookmark()
        {

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.Title = "test";

            //get a bookmark

            bookmarkRepo.Setup(x => x.GetBookmark("test")).Returns(new Bookmark { Title = "test"});

            var result = controller.GetBookmark("test");

            Assert.AreEqual(bookmark.Object.Title, result.Title);


        }

        [TestMethod]
        public void FavouriteBookmark_Success_ReturnUsers()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Returns(new List<User>());

            controller.FavouriteBookmark(1, 1);

            //test Save

            bookmarkRepo.Verify(m => m.Save());

        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void FavouriteBookmark_BookmarkIsNull_ThrowException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Throws(new HttpRequestException("Id not found"));

            controller.FavouriteBookmark(1, 1);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void FavouriteBookmark_UserIsNull_ThrowException()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Throws(new HttpRequestException("Id not found"));

            controller.FavouriteBookmark(1, 1);

            Assert.Fail();
        }

    }
}
