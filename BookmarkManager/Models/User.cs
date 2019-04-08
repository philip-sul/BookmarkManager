using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookmarkManager.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [EmailAddress]
        public string UserEmail { get; set; }

        public IList<Bookmark> Bookmarks { get; set; }

        public IList<Bookmark> FavouriteBookmarks { get; set; }
    }
}