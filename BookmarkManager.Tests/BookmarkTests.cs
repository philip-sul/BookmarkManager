using System;
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
        public void TestMethod1()
        {
            var studentRepo = new Mock<IBookmarkRepository>();

            var controller = new BookmarksController(studentRepo.Object);

            controller.CreateBookmark(new CreateJson()); //DeleteStudent(1);

            //studentRepo.Verify(m => m.DeleteStudent(1));

        }
    }
}
