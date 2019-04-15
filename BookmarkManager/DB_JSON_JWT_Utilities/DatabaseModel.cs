using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookmarkManager.Models
{
    /// <summary>
    /// BookmarkManager: DatabaseModel
    /// 
    /// Used to query and connect the db.
    /// 
    /// </summary>
    public class DatabaseModel : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Bookmark> Bookmarks { get; set; }

        public DatabaseModel() : base("name=defaultConnection")
        {

        }

    }
}