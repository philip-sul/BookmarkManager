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
        public UserController()
        {
            _userRepository = new UserRepository();
        }
        //possibly add other constructor.
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? new UserRepository();
        }

        [HttpGet]
        [Route("api/user/bookmarks/")]
        public IEnumerable<Bookmark> GetUserBookmarks(int userId, string token)
        {
            return _userRepository.GetUserBookmarks(userId, token);
        }

        [HttpGet]
        [Route("api/user/favorites/{userId}")]
        public IEnumerable<Bookmark> GetFavoriteBookmarks(int userId, string token)
        {
            return _userRepository.GetFavoriteBookmarks(userId, token);
        }

        [HttpPost]
        public User CreateUser(CreateUserJson content)
        {
            return _userRepository.CreateUser(content.User);
        }

        [HttpGet]
        [Route("api/user/login")]
        public string LoginUser(string username, string password)
        {
            return _userRepository.LoginUser(username, password);
        }

        [HttpDelete]
        public HttpStatusCode DeleteUser(int userId, string token)
        {
            return _userRepository.DeleteUser(userId, token);
        }

        [HttpGet]
        [Route("api/user/searchUsers")]
        public IEnumerable<User> SearchUsers(string username)
        {
            return _userRepository.SearchUsers(username);
        }

        public bool TestAuthenticate(int userId, string token)
        {
            return _userRepository.ValidateUser(userId, token);
        }
    }
}