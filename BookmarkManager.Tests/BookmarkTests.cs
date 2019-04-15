using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //get a bookmark

            Bookmark sample;

            bookmarkRepo.Setup(x => x.GetBookmark(1)).Returns(sample = new Bookmark { BookmarkId = 1 });

            var result = controller.GetBookmark(1);

            //check that are equal

            bool id = sample.BookmarkId == result.BookmarkId;
            bool author = sample.AuthorId == result.AuthorId;
            bool date = sample.Date == result.Date;
            bool link = sample.Link == result.Link;
            bool title = sample.Title == result.Title;
            bool users = sample.Users.Equals(result.Users);

            Assert.IsTrue(id && author && date && link && title && users);


        }

        [TestMethod]
        public void CreateBookmark_Success_VerifySave()
        {
            var bookmarkRepo = new Mock<IBookmarkRepository>();
            var controller = new BookmarksController(bookmarkRepo.Object);

            var bookmark = new Mock<Bookmark>();

            bookmarkRepo.Setup(x => x.CreateBookmark(bookmark.Object,"test"));

            controller.CreateBookmark(new CreateJson { Bookmark = bookmark.Object, Username = "test"});

            bookmarkRepo.Verify(r => r.Save());
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

            var newBookmark = new Bookmark()
            {
                BookmarkId = 1,
                AuthorId = 1,
                Date = DateTime.Now,
                Link = "link",
                Title = "title",
                Users = new List<User>()
            };

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = newBookmark.BookmarkId;
            bookmark.Object.AuthorId = newBookmark.AuthorId;
            bookmark.Object.Date = newBookmark.Date;
            bookmark.Object.Link = newBookmark.Link;
            bookmark.Object.Title = newBookmark.Title;
            bookmark.Object.Users = newBookmark.Users;

            //call bookmark successfully

            bookmarkRepo.Setup(x => x.CreateBookmark(bookmark.Object, "test")).Returns(newBookmark);

            var result = controller.CreateBookmark(new CreateJson {Bookmark = bookmark.Object, Username = "test" });

            bool id = newBookmark.BookmarkId == result.BookmarkId;
            bool author = newBookmark.AuthorId == result.AuthorId;
            bool date = newBookmark.Date == result.Date;
            bool link = newBookmark.Link == result.Link;
            bool title = newBookmark.Title == result.Title;
            bool users = newBookmark.Users.Equals(result.Users);

            Assert.IsTrue(id && author && date && link && title && users);

        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void EditBookmark_BookmarkIsNull_ThrowsException()
        {

            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            //call edit when bookmark is null to throw exception


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

            var newBookmark = new Bookmark()
            {
                BookmarkId = 1,
                AuthorId = 1,
                Date = DateTime.Now,
                Link = "link",
                Title = "title",
                Users = new List<User>()
            };

            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = newBookmark.BookmarkId;
            bookmark.Object.AuthorId = newBookmark.AuthorId;
            bookmark.Object.Date = newBookmark.Date;
            bookmark.Object.Link = newBookmark.Link;
            bookmark.Object.Title = newBookmark.Title;
            bookmark.Object.Users = newBookmark.Users;

            //get a bookmark that successfully returns HttpStatusCode

            bookmarkRepo.Setup(x => x.EditBookmark(1, bookmark.Object)).Returns(HttpStatusCode.Accepted);

            var result = controller.EditBookmark(new EditJson { Bookmark = bookmark.Object, UserId = 1 });

            Assert.AreEqual(result, HttpStatusCode.Accepted);
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

            var result = controller.DeleteBookmark(1);

            Assert.AreEqual(result, HttpStatusCode.Accepted);


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

            Bookmark sample;

            //get a bookmark successfully and test that Titles are the same

            bookmarkRepo.Setup(x => x.GetBookmark("test")).Returns(sample = new Bookmark { Title = "test" });

            var result = controller.GetBookmark("test");

            bool id = sample.BookmarkId == result.BookmarkId;
            bool author = sample.AuthorId == result.AuthorId;
            bool date = sample.Date == result.Date;
            bool link = sample.Link == result.Link;
            bool title = sample.Title == result.Title;
            bool users = sample.Users.Equals(result.Users);

            Assert.IsTrue(id && author && date && link && title && users);


        }

        [TestMethod]
        public void FavouriteBookmark_Success_ReturnUsers()
        {
            //create mock objects

            var bookmarkRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(bookmarkRepo.Object);

            List<User> sample;

            //call FavouriteBookmark and return list of users

            bookmarkRepo.Setup(x => x.FavouriteBookmark(1, 1)).Returns(sample = new List<User>());

            var result = controller.FavouriteBookmark(1, 1);

            bool users = sample.Equals(result);

            Assert.IsTrue(users);

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
