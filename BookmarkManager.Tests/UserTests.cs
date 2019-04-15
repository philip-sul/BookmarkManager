using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookmarkManager.Controllers;
using BookmarkManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace BookmarkManager.Tests
{
    /// <summary>
    /// BookmarkManager: UserTests
    /// Chris Masters - 4/14/2019
    /// 
    /// Performs various tests on the User functionalities. 
    /// 
    /// </summary>
    [TestClass]
    public class UserTests
    {
        //
        //CreateUser()
        //
        [TestMethod]
        public void CreateUser_Success_VerifySave()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.CreateUser(user.Object));
            userRepo.Verify(y => y.Save());

            controller.CreateUser(new CreateUserJson { User = new User() });

            userRepo.Verify(y => y.Save());
        }

        [TestMethod]
        public void CreateUser_Success_ReturnUser()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var sampleUser = new User()
            {
                UserId = 0,
                Username = "test1",
                UserPassword = "test2",
                UserEmail = "test3"
            };
            var user = new Mock<User>();

            user.Object.UserId = sampleUser.UserId;
            user.Object.Username = sampleUser.Username;
            user.Object.UserPassword = sampleUser.UserPassword;
            user.Object.UserEmail = sampleUser.UserEmail;

            userRepo.Setup(x => x.CreateUser(user.Object))
                .Returns(sampleUser);
            
            var expected = sampleUser;
            var actual = controller.CreateUser(new CreateUserJson(user.Object));

            bool value = (actual != null);
            bool id = (expected.UserId == actual.UserId);
            bool username = (expected.Username == actual.Username);
            bool password = (expected.UserPassword == actual.UserPassword);
            bool email = (expected.UserEmail == actual.UserEmail);

            Assert.IsTrue(value && id && username && password && email);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void CreateUser_Fail_InvalidInput_ThrowsEx()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);

            var user = new Mock<User>();

            userRepo.Setup(x => x.CreateUser(user.Object))
                .Throws(new HttpResponseException(HttpStatusCode.Conflict));

            controller.CreateUser(new CreateUserJson(user.Object));
            Assert.Fail();
        }

        //
        //GetUserBookmarks
        //
        [TestMethod]
        public void GetUserBookmarks_Success_ReturnBookmarks()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            bookmark.Object.AuthorId = 1;

            userRepo.Setup(x => x.GetUserBookmarks(1, "")).Returns(
                new List<Bookmark>(){
                    new Bookmark{
                        BookmarkId = 1,
                        Link = "x",
                        Date = DateTime.Now,
                        Title = "New",
                        AuthorId = 1
                    },
                    new Bookmark{
                        BookmarkId = 2,
                        Link = "x2",
                        Date = DateTime.Now,
                        Title = "New2",
                        AuthorId = 1
                    }
                });

            var result = controller.GetUserBookmarks(1, "");

            foreach (var item in result)
            {
                Assert.AreEqual(bookmark.Object.AuthorId, item.AuthorId);
            }
            
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void GetUserBookmarks_Fail_ThrowException()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            userRepo.Setup(x => x.GetUserBookmarks(1, ""))
                .Throws(new HttpResponseException(HttpStatusCode.Conflict));

            var result = controller.GetUserBookmarks(1, "");

            Assert.Fail();
        }


        //
        //GetFavoriteBookmarks
        //
        [TestMethod]
        public void GetFavoriteBookmarks_Success_ReturnBookmarks()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();
            var bookmark = new Mock<Bookmark>();

            bookmark.Object.BookmarkId = 1;
        }

        //
        //LoginUser
        //
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void LoginUser_Fail_InvalidCredentials_ThrowException()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.LoginUser(null, null))
                .Throws(new HttpResponseException(HttpStatusCode.Conflict));

            var result = controller.LoginUser(null, null);

            Assert.Fail();

        }


        [TestMethod]
        public void LoginUser_Success_ReturnUser()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.LoginUser("Chris", "Chris1")).Returns(
               "token");

            var result = controller.LoginUser("Chris", "Chris1");

            Assert.AreEqual(result, "token");
        }

        //
        //DeleteUser
        //
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteUser_Fail_Inauthentic()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.DeleteUser(1, "Chris1")).Throws(new HttpResponseException(HttpStatusCode.Conflict));
            var result = controller.DeleteUser(1, "Chris1");

            Assert.Fail();
        }

        [TestMethod]
        public void DeleteUser_Success_ReturnHttpStatusCode()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.DeleteUser(1, "Chris1")).Returns(HttpStatusCode.Accepted);
            var result = controller.DeleteUser(1, "Chris1");

            Assert.AreEqual(result, HttpStatusCode.Accepted);
        }

        //
        //SearchUsers
        //

        [TestMethod]
        public void SearchUsers_Success()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.SearchUsers("Chris")).Returns(
                new List<User>(){
                    new User{
                       UserId = 1,
                       Username = "Chris",
                       UserPassword = "Chris1",
                       UserEmail = "Chris@gmail.com"
                    }
                });

            var result = controller.SearchUsers("Chris");

            foreach (var item in result)
            {
                Assert.IsTrue(item.Username.Contains("Chris"));
            }
        }

        [TestMethod]
        public void SearchUsers_Fail()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);
            var user = new Mock<User>();

            userRepo.Setup(x => x.SearchUsers("Chris")).Returns(
                new List<User>(){
                    new User{
                       UserId = 0,
                       Username = "test",
                       UserPassword = "test",
                       UserEmail = "test"
                    }
                });

            var result = controller.SearchUsers("Chris");

            foreach (var item in result)
            {
                Assert.IsFalse(item.Username.Contains("Chris"));
            }
        }

        //Authentication

        [TestMethod]
        public void ValidateUser_Succes()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);

            userRepo.Setup(x => x.ValidateUser(1, "test")).Returns(true);

            var result = controller.TestAuthenticate(1, "test");

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void ValidateUser_Fail_exception()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController(userRepo.Object);

            var username = "real";
            userRepo.Setup(x => x.ValidateUser(1, "fake")).Throws(new HttpResponseException(HttpStatusCode.Conflict));

            var result = controller.TestAuthenticate(1, "fake");

            Assert.Fail();
        }

    }
}