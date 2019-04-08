using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookmarkManager.Models
{
    public class DatabaseModel : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Bookmark> Bookmarks { get; set; }

        public DatabaseModel() : base("name=defaultConnection")
        {

        }

    }
}