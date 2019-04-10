using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookmarkManager.Models
{
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }

        [Required]
        public string Link { get; set; }

        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public IList<User> Favourites { get; set; }

        public Bookmark()
        {
            Favourites = new List<User>();
        }

    }
}