using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookmarkManager.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateUser_Success_ReturnUser()
        {
            var userRepo = new Mock<IUserRepository>();
            var controller = new UserController();
            var user = new Mock<User>();

            userRepo.Setup(x => x.CreateUser(user.Object));
            userRepo.Verify(y => y.Save());

            controller.CreateUser(new CreateJson { });

            userRepo.Verify(y => y.Save());
        }

    }
}
