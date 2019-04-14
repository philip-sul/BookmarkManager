using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace BookmarkManager.Models
{
    public class UserRepository : IUserRepository
    {
        private DatabaseModel _dbContext;
        public UserRepository()
        {
            _dbContext = new DatabaseModel();
        }

        public User CreateUser(User user)
        {
            ValidateNewUser(user);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public HttpStatusCode DeleteUser(int userId)
        {
            ValidateUser(userId);

            _dbContext.Users.Remove(_dbContext.Users.First(x => x.UserId.Equals(userId)));
            Save();
            //signout
            return HttpStatusCode.Accepted;
        }

        public IEnumerable<Bookmark> GetFavoriteBookmarks(int userId)
        {
            ValidateUser(userId);
            var dbUser = _dbContext.Users.Find(userId);
            //maybe unneeded 
            if (dbUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            var userFavorites = dbUser.FavouriteBookmarks;
            //No exceptions, just return results
            return userFavorites;
        }

        public IEnumerable<Bookmark> GetUserBookmarks(int userId)
        {
            ValidateUser(userId);
            //maybe unneeded 
            var dbUser = _dbContext.Users.Find(userId);

            if (dbUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var userBookmarks = dbUser.Bookmarks;
            //No exceptions, just return results
            return userBookmarks;
        }

        public User LoginUser(User user)
        {
            var dbUser = _dbContext.Users.First(x => x.Username.Equals(user.Username));

            if(dbUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if(dbUser.UserPassword != user.UserPassword)
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }

            //generate token or w.e

            return dbUser;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void ValidateNewUser(User user)
        {

        }

        //authentication -- token
        public void ValidateUser(int userId)
        {
            //hardcode
            int key = userId;

            if (key != userId)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

        public IEnumerable<User> SearchUsers(string username)
        {
            var dbUsers = from u in _dbContext.Users
                          where u.Username.ToLower().Contains(username.ToLower())
                          select u;

            //No exceptions, just return results
            return dbUsers;
        }
    }


}