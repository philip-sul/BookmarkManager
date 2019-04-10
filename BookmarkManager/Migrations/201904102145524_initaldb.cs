namespace BookmarkManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initaldb : DbMigration
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
                        User_UserId1 = c.Int(),
                    })
                .PrimaryKey(t => t.BookmarkId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
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
            DropForeignKey("dbo.Bookmarks", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.Bookmarks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Bookmark_BookmarkId" });
            DropIndex("dbo.Bookmarks", new[] { "User_UserId1" });
            DropIndex("dbo.Bookmarks", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Bookmarks");
        }
    }
}
