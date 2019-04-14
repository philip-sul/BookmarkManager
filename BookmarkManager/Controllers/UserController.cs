using BookmarkManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookmarkManager.Controllers
{
    //add authentication
    public class UserController : ApiController
    {
        private IUserRepository _userRepository;

        //possibly add other constructor.
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? new UserRepository();
        }

        [HttpGet]
        public IEnumerable<Bookmark> GetUserBookmarks(int userId)
        {
            return _userRepository.GetUserBookmarks(userId);
        }

        [HttpGet]
        public IEnumerable<Bookmark> GetFavoriteBookmarks(int userId)
        {
            return _userRepository.GetFavoriteBookmarks(userId);
        }

        [HttpPost]
        public User CreateUser(CreateUserJson content)
        {
            return _userRepository.CreateUser(content.User);
        }

        [HttpGet]
        public User LoginUser(User user)
        {
            return _userRepository.LoginUser(user);
        }

        [HttpDelete]
        public HttpStatusCode DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }

        [HttpGet]
        public IEnumerable<User> SearchUsers(string username)
        {
            return _userRepository.SearchUsers(username);
        }

       
    }
}