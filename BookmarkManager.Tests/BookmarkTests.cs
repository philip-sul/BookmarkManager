using System;
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
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call a bookmark that throws an exception

            bookmarkRepo.Setup(x => x.GetBookmark(1)).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.GetBookmark(1);

            Assert.Fail();
        }

        [TestMethod]
        public void GetBookmark_WithIdSuccess_ReturnBookmark()
        {

            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;

            //get a bookmark

            bookmarkRepo.Setup(x => x.GetBookmark(1)).Returns(new Bookmark { BookmarkId = 1 });

            var result = controller.GetBookmark(1);

            //check that Ids are equal

            Assert.AreEqual(bookmark.Object.BookmarkId, result.BookmarkId);


        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void CreateBookmark_BookmarkIsNull_ThrowsException()
        {

            //make mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //create bookmark with null as Bookmark that will throw exception

            bookmarkRepo.Setup(x => x.CreateBookmark(null, "test")).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.CreateBookmark(new CreateJson { Bookmark = null, Username = "test" });

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void CreateBookmark_UserIsNotFound_ThrowsException()
        {

            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmarkRepo.Setup(x => x.CreateBookmark(bookmark.Object, "test")).Throws(new HttpRequestException("Username not found"));

            //needs refactoring to test

            controller.CreateBookmark(new CreateJson { Bookmark = new Bookmark(), Username = "test2" });

            Assert.Fail();
        }

        [TestMethod]
        public void CreateBookmark_Success_ReturnBookmark()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            //call bookmark successfully

            bookmarkRepo.Setup(x => x.CreateBookmark(bookmark.Object, "test")).Returns(bookmark.Object);

            controller.CreateBookmark(new CreateJson { Bookmark = new Bookmark(), Username = "test" });

            //test if Save was called

            bookmarkRepo.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void EditBookmark_BookmarkIsNull_ThrowsException()
        {

            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call edi when bookmark is null to throw exception

            bookmarkRepo.Setup(x => x.EditBookmark(1, null)).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.EditBookmark(new EditJson { Bookmark = null, UserId = 1 });

            Assert.Fail();
        }

        [TestMethod]
        public void EditBookmark_Success_ReturnBookmark()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;

            //get a bookmark that successfully returns HttpStatusCode

            bookmarkRepo.Setup(x => x.EditBookmark(1, bookmark.Object)).Returns(HttpStatusCode.Accepted);

            controller.EditBookmark(new EditJson { Bookmark = new Bookmark(), UserId = 1 });

            //test if Save() was called

            bookmarkRepo.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteBookmark_BookmarkNotFound_ThrowsException()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call delete that will throw exception

            bookmarkRepo.Setup(x => x.DeleteBookmark(1)).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.DeleteBookmark(1);

            Assert.Fail();
        }

        [TestMethod]
        public void DeleteBookmark_Success_ReturnHttpStatusCode()
        {

            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call delete with successful HttpStatusCode


            bookmarkRepo.Setup(x => x.DeleteBookmark(1)).Returns(HttpStatusCode.Accepted);

            controller.DeleteBookmark(1);

            //test if Save() was called

            bookmarkRepo.Verify(m => m.Save());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetBookmark_NoTitle_ThrowException()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call get bookmark that will throw exception

            bookmarkRepo.Setup(x => x.GetBookmark("test")).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.GetBookmark("test");

            Assert.Fail();

        }

        [TestMethod]
        public void GetBookmark_WithTitleSuccess_ReturnBookmark()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.Title = "test";

            //get a bookmark successfully and test that Titles are the same

            bookmarkRepo.Setup(x => x.GetBookmark("test")).Returns(new Bookmark { Title = "test" });

            var result = controller.GetBookmark("test");

            Assert.AreEqual(bookmark.Object.Title, result.Title);


        }

        [TestMethod]
        public void FavouriteBookmark_Success_ReturnUsers()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call FavouriteBookmark and return list of users

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Returns(new List<User>());

            controller.FavouriteBookmark(1, 1);

            //test if Save() was called

            bookmarkRepo.Verify(m => m.Save());

        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void FavouriteBookmark_BookmarkIsNull_ThrowException()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //needs refactoring

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Throws(new HttpRequestException("Id not found"));

            controller.FavouriteBookmark(1, 1);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void FavouriteBookmark_UserIsNull_ThrowException()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //needs refactoring

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Throws(new HttpRequestException("Id not found"));

            controller.FavouriteBookmark(1, 1);

            Assert.Fail();
        }

    }
}
