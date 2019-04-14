using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkManager.Models
{
    public interface IUserRepository
    {
        IEnumerable<Bookmark> GetUserBookmarks(int userId);
        IEnumerable<Bookmark> GetFavoriteBookmarks(int userId);
        User CreateUser(User user);
        User LoginUser(User user);
        HttpStatusCode DeleteUser(int userId);
        IEnumerable<User> SearchUsers(string username);

        //maybe move to a new interface
        void ValidateNewUser(User user);
        void ValidateUser(int id);
        void Save();
    }
}
