using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookmarkManager.Models
{
    public class Bookmark
    {
        [Key]
        public int? BookmarkId { get; set; }

        [Required]
        public string Link { get; set; }

        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [InverseProperty("Bookmarks")]
        [Required]
        public int AuthorId { get; set; }

        [InverseProperty("FavouriteBookmarks")]
        public IList<User> Users { get; set; }


        public Bookmark()
        {

            Users = new List<User>();

        }

    }
}