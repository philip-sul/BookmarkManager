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
                        User_UserId = c.Int(),
                        User_UserId1 = c.Int(),
                        Author_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookmarkId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .ForeignKey("dbo.Users", t => t.Author_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1)
                .Index(t => t.Author_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        UserPassword = c.String(nullable: false),
                        UserEmail = c.String(),
                        Bookmark_BookmarkId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Bookmarks", t => t.Bookmark_BookmarkId)
                .Index(t => t.Bookmark_BookmarkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Bookmark_BookmarkId", "dbo.Bookmarks");
            DropForeignKey("dbo.Bookmarks", "Author_UserId", "dbo.Users");
            DropForeignKey("dbo.Bookmarks", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.Bookmarks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Bookmark_BookmarkId" });
            DropIndex("dbo.Bookmarks", new[] { "Author_UserId" });
            DropIndex("dbo.Bookmarks", new[] { "User_UserId1" });
            DropIndex("dbo.Bookmarks", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Bookmarks");
        }
    }
}
