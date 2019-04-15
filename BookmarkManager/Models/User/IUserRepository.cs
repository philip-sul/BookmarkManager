using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkManager.Models
{
    /// <summary>
    /// BookmarkManager: IUserRepository
    /// Chris Masters - 4/14/2019
    /// 
    /// Used in dep. injection for userRepo and UserController.
    /// 
    /// </summary>
    public interface IUserRepository
    {
        IEnumerable<Bookmark> GetUserBookmarks(int userId, string token);
        IEnumerable<Bookmark> GetFavoriteBookmarks(int userId, string token);
        User CreateUser(User user);
        string LoginUser(string username, string password);
        HttpStatusCode DeleteUser(int userId, string token);
        IEnumerable<User> SearchUsers(string username);
        bool ValidateUser(int id, string token);
        void Save();
    }
}
