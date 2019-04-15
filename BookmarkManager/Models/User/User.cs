using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BookmarkManager.Models
{
    /// <summary>
    /// BookmarkManager: User
    /// Chris Masters - 4/14/2019
    /// 
    /// Structures the user class and Models the User table in db.
    /// Properties are annotated for db and Jwt
    /// 
    /// </summary>
    public class User
    {
        [Key]
        public int? UserId { get; set; }

        [Required]
        public string Username { get; set; }

       // [JsonIgnore] - removed because was causing issues with creating new User
        [Required]
        public string UserPassword { get; set; }

        [EmailAddress]
        [Required]
        public string UserEmail { get; set; }

        public  IList<Bookmark> Bookmarks { get; set; }

        [InverseProperty("Users")]
        public IList<Bookmark> FavouriteBookmarks { get; set; }

        public User()
        {
            Bookmarks = new List<Bookmark>();

            FavouriteBookmarks = new List<Bookmark>();

        }

    }
}