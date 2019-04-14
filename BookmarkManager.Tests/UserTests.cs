using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BookmarkManager.Controllers;
using BookmarkManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace BookmarkManager.Tests
{
    [TestClass]
    public class UserTests
    {
        //CreateUser()
        [TestMethod]
        public void CreateUser_Success_ReturnUser()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.CreateUser(user.Object));
            userRepo.Verify(y => y.Save());

            controller.CreateUser(new CreateUserJson { User = new User() });

            userRepo.Verify(y => y.Save());
        }

        //GetUserBookmarks
        [TestMethod]
        public void GetUserBookmarks_Success_ReturnBookmarks()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;

            userRepo.Setup(x => x.GetUserBookmarks(1)).Returns(
                new List<Bookmark>(){
                    new Bookmark{
                        BookmarkId = 1,
                        Link = "x",
                        Date = DateTime.Now,
                        Title = "New"
                    }
                });

            var result = controller.GetUserBookmarks(1);
            var item = result.First().BookmarkId;

            Assert.AreEqual(bookmark.Object.BookmarkId, item);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetUserBookmarks_Fail_ThrowException()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            //bookmark.Object.BookmarkId = 1;

            userRepo.Setup(x => x.GetUserBookmarks(1))
                .Throws(new HttpResponseException(HttpStatusCode.Conflict));

            var result = controller.GetUserBookmarks(1);


            Assert.Fail();
        }


        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetUserBookmarks_Fail_Inauthentic_ThrowException()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            ////bookmark.Object.BookmarkId = 1;

            //userRepo.Setup(x => x.GetUserBookmarks(1))
            //    .Throws(new HttpResponseException(HttpStatusCode.Conflict));

            //var result = controller.GetUserBookmarks(1);


            //Assert.Fail();
        }

        //GetFavoriteBookmarks
        [TestMethod]
        public void GetFavoriteBookmarks_Success_ReturnBookmarks()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;

            
        }


        //LoginUser
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void LoginUser_Fail_InvalidUsername_ThrowException()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void LoginUser_Fail_InvalidPassword_ThrowException()
        {

        }

        [TestMethod]
        public void LoginUser_Success_ReturnUser()
        {

        }

        //DeleteUser
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteUser_Fail_InvalidUserId_ThrowException()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteUser_Fail_inauthentic_ThrowException()
        {

        }

        [TestMethod]
        public void DeleteUser_Success_ReturnHttpStatusCode()
        {

        }
        //SearchUsers
    }
}