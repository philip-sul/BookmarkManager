using Lab6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace BookmarkManager.Models
{
    /// <summary>
    /// 
    /// BookmarkManager: UserRepository
    /// Chris Masters - 4/14/2019
    /// 
    /// The UserRepository is used to perform the business logic nessisary for the User functions.
    /// Allows for the dependency injection into UserController with the IUserRepository.
    /// Accesses the database via _dbContext.
    /// Generates the authentication token on login.
    /// 
    /// Authentication has not yet been fully implemented, checking the token for the matching
    /// key.
    /// 
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private DatabaseModel _dbContext;
        public UserRepository()
        {
            _dbContext = new DatabaseModel();
        }

        public User CreateUser(User user)
        {
            var verify = _dbContext.Users.FirstOrDefault(x => x.Username.Equals(user.Username));
            if(verify != null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            verify = _dbContext.Users.FirstOrDefault(x => x.UserEmail.Equals(user.UserEmail));
            if (verify != null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            _dbContext.Users.Add(user);
            Save();

            //Hardcoded because JsonIgnore was causing problems
            user.UserPassword = "";
            return user;
        }

        public HttpStatusCode DeleteUser(int userId, string token)
        {
            ValidateUser(userId, token);

            _dbContext.Users.Remove(_dbContext.Users.FirstOrDefault(x => x.UserId.Equals(userId)));
            Save();
            //signout
            return HttpStatusCode.Accepted;
        }

        public IEnumerable<Bookmark> GetFavoriteBookmarks(int userId, string token)
        {
            ValidateUser(userId, token);

            var dbUser = _dbContext.Users.Find(userId);
           
            if (dbUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
            
            var userFavorites = dbUser.FavouriteBookmarks;
            
            return userFavorites;
        }

        public IEnumerable<Bookmark> GetUserBookmarks(int userId, string token)
        {
            ValidateUser(userId, token);
            
            var dbUser = _dbContext.Users.Find(userId);

            if (dbUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
            
            var userBookmarks = dbUser.Bookmarks;
            
            return userBookmarks;
        }

        public string LoginUser(string username, string password)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(x => x.Username.Equals(username));

            if(dbUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
            if(dbUser.UserPassword != password)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            return JwtManager.GenerateToken(dbUser.Username, 20);
           
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        //authentication -- token
        //Currently cant figure out how to extract the username(key) from token
        public bool ValidateUser(int userId, string token)
        {
            var username = _dbContext.Users.Find(userId).Username;

            if(username == null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            //var tokenUsername = JwtManager.GetPrincipal(token).ToString();

            //hardcoded
            var tokenUsername = username;

            if (tokenUsername != username || tokenUsername == null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
            return true;
        }

        public IEnumerable<User> SearchUsers(string username)
        {
            var dbUsers = from u in _dbContext.Users
                          where u.Username.ToLower().Contains(username.ToLower())
                          select u;

            return dbUsers;
        }
    }


}