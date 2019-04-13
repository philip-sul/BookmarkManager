namespace BookmarkManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookmarkId = c.Int(nullable: false, identity: true),
                        Link = c.String(nullable: false),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BookmarkId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        UserPassword = c.String(nullable: false),
                        UserEmail = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.BookmarkUsers",
                c => new
                    {
                        Bookmark_BookmarkId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bookmark_BookmarkId, t.User_UserId })
                .ForeignKey("dbo.Bookmarks", t => t.Bookmark_BookmarkId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Bookmark_BookmarkId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookmarkUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.BookmarkUsers", "Bookmark_BookmarkId", "dbo.Bookmarks");
            DropForeignKey("dbo.Bookmarks", "User_UserId", "dbo.Users");
            DropIndex("dbo.BookmarkUsers", new[] { "User_UserId" });
            DropIndex("dbo.BookmarkUsers", new[] { "Bookmark_BookmarkId" });
            DropIndex("dbo.Bookmarks", new[] { "User_UserId" });
            DropTable("dbo.BookmarkUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Bookmarks");
        }
    }
}
