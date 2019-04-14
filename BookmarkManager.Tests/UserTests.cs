using System;
using BookmarkManager.Controllers;
using BookmarkManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace BookmarkManager.Tests
{
    [TestClass]
    public class UserTests
    {
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

    }
}
